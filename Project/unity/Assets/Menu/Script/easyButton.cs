using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ RequireComponent( typeof( AudioSource ) ) ]

public class easyButton : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 1.0f;
	private Color color;
	
	// animation
	private float timeBetweenAnimationS = 0.05f;
	private float timeBetweenAnimationUnS = 0.05f;
	private float timeLeftAnimationChange = 0;
	private bool active;
	private bool animationForward;
	private int animationIndex;
	public List<Texture2D> texturesS;
	public List<Texture2D> texturesUnS;
	public normalButton normalButton;
	public hardButton hardButton;
	private List<Texture2D> currentAnimation;
	public AudioClip level_easy;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.10f;
		initPixelInset.width = initPixelInset.height*3f;
		initPixelInset.x = -Screen.width*0.15f;
		initPixelInset.y = -Screen.height*0.475f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		active = false;
		animationForward = true;
		animationIndex = 0;
		currentAnimation = texturesUnS;
	}
	
	void Update(){
		delayToAppear = Mathf.Max(0,delayToAppear-Mathf.Abs(Time.deltaTime));
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
		
		// animation
		timeLeftAnimationChange = Mathf.Max(0,timeLeftAnimationChange-Mathf.Abs(Time.deltaTime));
		if(timeLeftAnimationChange <= 0){
			if(active){
				timeLeftAnimationChange = timeBetweenAnimationS;
			}else{
				timeLeftAnimationChange = timeBetweenAnimationUnS;
			}
			if(animationForward){
				if(animationIndex < currentAnimation.Count-1){
					animationIndex++;
				}else{
					animationIndex=currentAnimation.Count-2;
					animationForward = false;
				}
			}else{
				if(animationIndex > 0){
					animationIndex--;
				}else{
					animationIndex=1;
					animationForward = true;
				}
			}
			guiTexture.texture = currentAnimation[animationIndex];
		}
	}
	
	public void noActive(){
		active = false;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		currentAnimation = texturesUnS;
	}
	
	public void OnMouseUpAsButton(){
		audio.PlayOneShot(level_easy);
		this.active = true;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationS;
		currentAnimation = texturesS;

		normalButton.noActive();
		hardButton.noActive();
		// normal button NO active
		// hard button NO active


		PlayerPrefs.SetString("easy", "y");
		PlayerPrefs.SetString("normal", "n");
		PlayerPrefs.SetString("hard", "n");
		// save the easy mode active
		// save the easy mode NO active
		// save the easy mode NO active
	}
}
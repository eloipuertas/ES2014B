using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class main_exit_button : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 2.0f;
	private float delayToAppear = 1.0f;
	private Color color;
	
	// animation
	private float timeBetweenAnimationS = 0.05f;
	private float timeBetweenAnimationUnS = 0.05f;
	private float timeLeftAnimationChange = 0;
	private bool selected;
	private bool animationForward;
	private int animationIndex;
	public List<Texture2D> texturesS;
	public List<Texture2D> texturesUnS;
	private List<Texture2D> currentAnimation;

	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width*0.18f;
		initPixelInset.height = initPixelInset.width/3f;
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = -Screen.height*0.25f-initPixelInset.height*2.0f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		selected = false;
		animationForward = true;
		animationIndex = 0;
		currentAnimation = texturesUnS;
	}
	
	void OnGUI(){
		Time.timeScale = 1;
		delayToAppear = Mathf.Max(0,delayToAppear-Time.deltaTime);
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
		
		// animation
		timeLeftAnimationChange = Mathf.Max(0,timeLeftAnimationChange-Mathf.Abs(Time.deltaTime));
		if(timeLeftAnimationChange <= 0){
			if(selected){
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
	
	public void OnMouseExit(){
		selected = false;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationUnS;
		currentAnimation = texturesUnS;
	}
	
	public void OnMouseEnter(){
		selected = true;
		animationIndex=0;
		timeLeftAnimationChange = timeBetweenAnimationS;
		currentAnimation = texturesS;
	}
	
	public void OnMouseUpAsButton(){
		Application.Quit();
	}
}

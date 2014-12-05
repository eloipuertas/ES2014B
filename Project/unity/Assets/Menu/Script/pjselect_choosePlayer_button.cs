using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pjselect_choosePlayer_button : MonoBehaviour{

	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 0.5f;
	private float delayToAppear = 0.5f;
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
	
	public bool first;
	public bool second;
	public bool third;
	private GameObject jug = null;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width*0.125f;
		initPixelInset.height = initPixelInset.width/3f;
		initPixelInset.x = -Screen.width*0.2375f;
		initPixelInset.y = -Screen.height*0.250f;
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

	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){
		if(first){
			PlayerPrefs.SetString("player", "player1");
			//jug = (GameObject)Instantiate (Resources.Load ("player1"));
		}else if(second){
			PlayerPrefs.SetString("player", "player2");
			//jug = (GameObject)Instantiate (Resources.Load ("player2"));
		}else if(third){
			PlayerPrefs.SetString("player", "player3");
			//jug = (GameObject)Instantiate (Resources.Load ("player3"));
		}
		Application.LoadLevel (2); //Load the game (next scene)
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bird_anim : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	public float secondsToAppear = 1.0f;
	public float delayToAppear = 1.0f;
	private Color color;
	
	// animation
	public float timeBetweenAnimation = 0.05f;
	private float timeLeftAnimationChange = 0;
	private bool animationForward;
	private int animationIndex;
	public List<Texture2D> textures;
	public float initPosPercentX = 0;
	public float initPosPercentY = 0;
	public float initWPercent = 0.10f;
	public float initHPercent = 0.25f;
	public float endPosPercentX = 0;
	public float endPosPercentY = 0;
	private float vel = 0.5f;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		float rand = Random.Range(0.3f,1.2f);
		initPixelInset.height = Screen.height*initHPercent*rand;
		initPixelInset.width = Screen.width*initWPercent*rand;
		initPixelInset.x = Screen.width*(-Random.Range(-1.05f,1.50f));
		initPixelInset.y = Screen.height*(-Random.Range(-0.5f,0.5f));
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimation;
		animationForward = true;
		animationIndex = Random.Range(0,textures.Count-1);
		// move animation
		vel = Random.Range(0.4f,0.9f);
	}
	
	void Update(){
		delayToAppear = Mathf.Max(0,delayToAppear-Mathf.Abs(Time.deltaTime));
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}

		// move animation
		Rect initPixelInset = guiTexture.pixelInset;
		if(initPixelInset.x > Screen.width*1.05f){
			initPixelInset.x = Screen.width*(-Random.Range(1.05f,1.15f));
			initPixelInset.y = Screen.height*(-Random.Range(-0.5f,0.5f));
			float rand = Random.Range(0.3f,1.2f);
			initPixelInset.height = Screen.height*initHPercent*rand;
			initPixelInset.width = Screen.width*initWPercent*rand;
			vel = Random.Range(0.4f,0.9f);
		}else{
			initPixelInset.x += vel;
		}
		print("X BIRD "+initPixelInset+" Screen.width*1.05f "+Screen.width*1.05f);
		guiTexture.pixelInset = initPixelInset;

		
		// animation
		timeLeftAnimationChange = Mathf.Max(0,timeLeftAnimationChange-Mathf.Abs(Time.deltaTime));
		if(timeLeftAnimationChange <= 0){
			timeLeftAnimationChange = timeBetweenAnimation;
			if(animationForward){
				if(animationIndex < textures.Count-1){
					animationIndex++;
				}else{
					animationIndex=0;
					animationForward = true;
				}
			}else{
				if(animationIndex > 0){
					animationIndex--;
				}else{
					animationIndex=1;
					animationForward = true;
				}
			}
			guiTexture.texture = textures[animationIndex];
		}
	}
}
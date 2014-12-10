using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fireAnim : MonoBehaviour {
	
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
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*initHPercent;
		initPixelInset.width = Screen.width*initWPercent;
		initPixelInset.x = Screen.width*initPosPercentX;
		initPixelInset.y = Screen.height*initPosPercentY;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimation;
		animationForward = true;
		animationIndex = Random.Range(0,textures.Count-1);
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
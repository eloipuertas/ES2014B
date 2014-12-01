using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class main_title : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 1.0f;
	public Texture2D texture;
	private Color color;
	
	void Awake(){
		Time.timeScale = 1;
		guiTexture.texture = texture;

		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.60f;	// 300
		initPixelInset.width = initPixelInset.height*2f;	// 600
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = 0-initPixelInset.height*0.00f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
	}
	
	void OnGUI(){
		delayToAppear = Mathf.Max(0,delayToAppear-Time.deltaTime);
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
	}
}
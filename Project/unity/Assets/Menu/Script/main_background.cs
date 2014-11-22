using UnityEngine;
using System.Collections;

public class main_background : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	public float secondsToAppear = 1.0f;
	public float delayToAppear = 0.1f;
	public Texture2D texture;
	public Color color;
	
	void Start(){
		guiTexture.texture = texture;
		
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width;
		initPixelInset.height = Screen.height;
		initPixelInset.x = -Screen.width*0.5f;
		initPixelInset.y = -Screen.height*0.5f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		color.r = 0;
		color.g = 0;
		color.b = 0;
		guiTexture.color = color;
	}
	
	void OnGUI(){
		delayToAppear = Mathf.Max(0,delayToAppear-Time.deltaTime);
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(MAX_COLOR_VAL,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			color.r = Mathf.Min(MAX_COLOR_VAL,color.r+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			color.g = Mathf.Min(MAX_COLOR_VAL,color.g+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			color.b = Mathf.Min(MAX_COLOR_VAL,color.b+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
	}
}
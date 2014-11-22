using UnityEngine;
using System.Collections;

public class pjselect_pjbackground2 : MonoBehaviour{
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 0.75f;
	public Texture2D texture;
	private Color color;
	
	void Awake(){
		Time.timeScale = 1;
		guiTexture.texture = texture;
		
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width*0.25f;
		initPixelInset.height = Screen.height*0.70f;
		initPixelInset.x = -Screen.width*0.125f;
		initPixelInset.y = -Screen.height*0.30f;
		guiTexture.pixelInset = initPixelInset;
		color = guiTexture.color;
		color.a = 0;
		guiTexture.color = color;
	}
	
	void Update(){
		delayToAppear = Mathf.Max(0,delayToAppear-Mathf.Abs(Time.deltaTime));
		if(delayToAppear <= 0){
			color = guiTexture.color;
			color.a = Mathf.Min(1,color.a+(MAX_COLOR_VAL/secondsToAppear)*Time.deltaTime);
			guiTexture.color = color;
		}
	}
}
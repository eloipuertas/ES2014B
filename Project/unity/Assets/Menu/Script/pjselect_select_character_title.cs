using UnityEngine;
using System.Collections;

public class pjselect_select_character_title : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 1.0f;
	public Texture2D texture;
	private Color color;
	
	void Awake(){
		Time.timeScale = 1;
		guiTexture.texture = texture;
		
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.075f;
		initPixelInset.width = initPixelInset.height*3f;
		initPixelInset.x = -initPixelInset.width*0.5f;
		initPixelInset.y = Screen.height*0.4125f;
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
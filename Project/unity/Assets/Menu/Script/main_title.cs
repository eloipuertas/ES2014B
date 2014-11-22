using UnityEngine;
using System.Collections;

public class main_title : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	public float secondsToAppear = 2.0f;
	public float delayToAppear = 1.0f;
	public Texture2D texture;
	public Color color;
	
	void Start(){
		guiTexture.texture = texture;

		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.40f;	// 300
		initPixelInset.width = initPixelInset.height*2f;	// 600
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = 0-initPixelInset.height*0.05f;
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
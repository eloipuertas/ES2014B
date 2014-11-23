using UnityEngine;
using System.Collections;

public class pjselect_back_button : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 1.0f;
	private float delayToAppear = 1.0f;
	public Texture2D textureS;
	public Texture2D textureUnS;
	private Color color;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.10f;
		initPixelInset.width = initPixelInset.height*3f;
		initPixelInset.x = -Screen.width*0.40f;
		initPixelInset.y = -Screen.height*0.475f;
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
	
	public void OnMouseExit(){
		guiTexture.texture = textureUnS;
	}
	
	public void OnMouseEnter(){
		guiTexture.texture = textureS;
	}
	
	public void OnMouseUpAsButton(){
		Application.LoadLevel(0); //Load the main menu (prev scene)
	}
}

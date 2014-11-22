using UnityEngine;
using System.Collections;

public class main_exit_button : MonoBehaviour {
	
	private static float MAX_COLOR_VAL = 0.5f;
	public float secondsToAppear = 4.0f;
	public float delayToAppear = 2.5f;
	public Texture2D textureS;
	public Texture2D textureUnS;
	public Color color;

	void Start(){
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width*0.18f;
		initPixelInset.height = initPixelInset.width/3f;
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = -Screen.height*0.25f-initPixelInset.height*1.5f;
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
	
	public void OnMouseExit(){
		guiTexture.texture = textureUnS;
	}
	
	public void OnMouseEnter(){
		guiTexture.texture = textureS;
	}
	
	public void OnMouseUpAsButton(){
		Application.Quit();
	}
}

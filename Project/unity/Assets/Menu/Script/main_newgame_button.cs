using UnityEngine;
using System.Collections;

public class main_newgame_button : MonoBehaviour {
	
	public Texture2D textureS;
	public Texture2D textureUnS;
	
	void Awake(){
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = 450*0.5f;
		initPixelInset.height = 150*0.5f;
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = 0-initPixelInset.height*1.0f;
		guiTexture.pixelInset = initPixelInset;
	}
	
	public void OnMouseExit(){
		guiTexture.texture = textureUnS;
	}
	
	public void OnMouseEnter(){
		guiTexture.texture = textureS;
	}
	
	public void OnMouseUpAsButton(){
		Application.LoadLevel(1); //Load the pjselect (next scene)
	}
}

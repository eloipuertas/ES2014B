using UnityEngine;
using System.Collections;

public class main_title : MonoBehaviour {
		
	public Texture2D texture;
	
	void Awake(){
		guiTexture.texture = texture;

		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.height*0.95f;	// 600
		initPixelInset.height = initPixelInset.width*0.5f;	// 300
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = 0-initPixelInset.height*0.0f;
		guiTexture.pixelInset = initPixelInset;
	}
}
using UnityEngine;
using System.Collections;

public class main_title : MonoBehaviour {
		
	public Texture2D texture;
	
	void Awake(){
		guiTexture.texture = texture;

		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.height = Screen.height*0.40f;	// 300
		initPixelInset.width = initPixelInset.height*2f;	// 600
		initPixelInset.x = 0-initPixelInset.width*0.5f;
		initPixelInset.y = 0-initPixelInset.height*0.05f;
		guiTexture.pixelInset = initPixelInset;
	}
}
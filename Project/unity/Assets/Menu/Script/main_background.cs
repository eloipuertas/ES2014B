using UnityEngine;
using System.Collections;

public class main_background : MonoBehaviour {
	
	public Texture2D texture;
	
	void Awake(){
		guiTexture.texture = texture;
		
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width;
		initPixelInset.height = Screen.height;
		initPixelInset.x = -Screen.width*0.5f;
		initPixelInset.y = -Screen.height*0.5f;
		guiTexture.pixelInset = initPixelInset;
	}
}
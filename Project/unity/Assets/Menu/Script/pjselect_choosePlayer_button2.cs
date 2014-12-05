using UnityEngine;
using System.Collections;

public class pjselect_choosePlayer_button2 : MonoBehaviour{

	private static float MAX_COLOR_VAL = 0.5f;
	private float secondsToAppear = 0.5f;
	private float delayToAppear = 0.5f;
	public Texture2D textureS;
	public Texture2D textureUnS;
	private Color color;
	
	public bool first;
	public bool second;
	public bool third;
	private GameObject jug = null;
	
	void Awake(){
		Time.timeScale = 1;
		Rect initPixelInset = new Rect(0,0,1,1);
		initPixelInset.width = Screen.width*0.125f;
		initPixelInset.height = initPixelInset.width/3f;
		initPixelInset.x = Screen.width*0.1150f;
		initPixelInset.y = -Screen.height*0.250f;
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

	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){
		if(first){
			PlayerPrefs.SetString("player", "player1");
			//jug = (GameObject)Instantiate (Resources.Load ("player1"));
		}else if(second){
			PlayerPrefs.SetString("player", "player2");
			//jug = (GameObject)Instantiate (Resources.Load ("player2"));
		}else if(third){
			PlayerPrefs.SetString("player", "player3");
			//jug = (GameObject)Instantiate (Resources.Load ("player3"));
		}
		Application.LoadLevel (2); //Load the game (next scene)
	}
}

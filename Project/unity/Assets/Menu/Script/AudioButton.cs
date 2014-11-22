using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AudioButton : MonoBehaviour{
	
	public AudioClip button;
	
	public bool isOver = false;
	public bool isPressed = false;

	void OnMouseOver(){
		if(!isOver){
			audio.PlayOneShot(button);
			isOver = true;
		}
		if (!isPressed){
			audio.PlayOneShot(button);
		}

		isPressed = true;

	}

	void OnMouseExit(){
		if(isOver){
			//audio.PlayOneShot(button);
			isOver = false;
		}

		isPressed = false;

	}
}
using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AudioButton : MonoBehaviour 
{
	
	public AudioClip button;

	public bool isPressed = false;

	void OnMouseOver () 
	{
		if ( !isPressed )
			audio.PlayOneShot( button );

		isPressed = true;

	}

	void OnMouseExit ()
	{
		audio.PlayOneShot( button );

		isPressed = false;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AmbientalMusic : MonoBehaviour {

	public AudioClip catacumba;
	public AudioClip fight;

	bool PNJinScene = false;

	// Use this for initialization
	void Start () 
	{
		audio.clip = catacumba;
		audio.loop = true;

		audio.Play ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		PNJinScene = isPNJinScene();

		if ( PNJinScene ) 
		{
			audio.clip = fight;
			audio.Play ();

		} else {
			audio.clip = catacumba;
			audio.Play ();

		}
	
	}

	bool isPNJinScene()
	{
		// TODO
	}
}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AmbientalMusic : MonoBehaviour {

	public AudioClip gotes;
	public AudioClip ratPanat;
	public AudioClip correntAire;
	public AudioClip correntAigua;

	public AudioClip fight;

	bool PNJinScene = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		PNJinScene = isPNJinScene();

		if ( PNJinScene ) {
				audio.clip = fight;
				audio.Play ();

		} else {

		}
	
	}

	bool isPNJinScene()
	{
		// TODO
	}
}

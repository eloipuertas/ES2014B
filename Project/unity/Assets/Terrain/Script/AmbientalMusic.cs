using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AmbientalMusic : MonoBehaviour 
{
	public AudioClip catacumba;
	public AudioClip fight;

	public AudioClip gameOver;

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
		if ( ! GameObject.FindWithTag ("Player").activeInHierarchy ) 
		{
			audio.clip = gameOver;

			if ( ! audio.isPlaying ) audio.Play ();

		}

		/*else if ( GameObject.FindWithTag ( "Spider" ).activeInHierarchy ) 
		{
			audio.clip = fight;

			if ( ! audio.isPlaying ) audio.Play ();

		} */

		else {
			audio.clip = catacumba;

			if ( ! audio.isPlaying ) audio.Play ();

		}
	
	}
}

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
		GameObject player = GameObject.FindWithTag ("Player");
		GameObject spider = GameObject.FindWithTag ("Spider");
		if (player == null || spider == null) {
			audio.clip = catacumba;
			if ( ! audio.isPlaying ) audio.Play ();
			return;
		}
		if ( ! GameObject.FindWithTag ("Player").activeInHierarchy ) 
		{
			audio.clip = gameOver;

			if ( ! audio.isPlaying ) audio.Play ();

		} else if ( spider.activeInHierarchy ){
			audio.clip = fight;

			if ( ! audio.isPlaying ) audio.Play ();

		} else {
			audio.clip = catacumba;

			if ( ! audio.isPlaying ) audio.Play ();

		}
	
	}
}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AmbientalMusic : MonoBehaviour 
{
	public GameObject PNJ;

	public AudioClip catacumba;
	public AudioClip fight;

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
		if ( GameObject.Find ( "SPIDER" ).activeInHierarchy ) 
		{
			audio.clip = fight;

			if ( ! audio.isPlaying ) audio.Play ();

		} else {
			audio.clip = catacumba;

			if ( ! audio.isPlaying ) audio.Play ();

		}
	
	}
}

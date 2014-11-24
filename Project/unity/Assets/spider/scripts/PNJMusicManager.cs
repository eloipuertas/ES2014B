using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PNJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;

	public AudioClip PNJKilled;

	public void PlayAttackOK()
	{
		if (!audio.isPlaying) {
				audio.clip = attackOK;
				audio.Play ();
		}

	}
	
	public void PlayPNJKilled()
	{
		audio.clip = PNJKilled;
		audio.Play ();		

	}

}

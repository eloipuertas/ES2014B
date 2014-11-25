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
			Debug.Log("PNJMusicManager: PlayAttackOK");
				audio.loop = false;
				audio.clip = attackOK;
				audio.Play ();
		}

	}
	
	public void PlayPNJKilled()
	{
		Debug.Log("PNJMusicManager: PlayPNJKilled");
		audio.clip = PNJKilled;
		audio.Play ();		

	}

}

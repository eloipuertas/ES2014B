using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PNJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;

	public AudioClip PNJKilled;

	public AudioClip OKill;
	public AudioClip OHurt;
	public AudioClip[] OHit;

	public void PlayAttackOK()
	{
		if (!audio.isPlaying) {
			audio.loop = false;
			audio.clip = attackOK;
			audio.Play ();
		}

	}
	
	public void PlayPNJKilled()
	{
		audio.clip = PNJKilled;
		audio.Play ();		

	}
	
	public void PlayOgreKilled()
	{
		audio.clip = OKill;
		audio.Play ();		
		
	}
	
	public void PlayOgreHurt()
	{
		audio.clip = OHurt;
		audio.Play ();		
		
	}
	
	public void PlayOgreHit()
	{
		audio.clip = OHit[Random.Range(0, OHit.Length)];;
		audio.Play ();		
		
	}

}

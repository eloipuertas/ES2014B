using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PNJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;
	public AudioClip criticalAttack;

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

	public void PlayCriticalAttack()
	{
		if (!audio.isPlaying) {
			audio.loop = false;
			audio.clip = criticalAttack;
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
		int i = Random.Range(0, OHit.Length - 1);
		Debug.Log ("OHit.Length " + OHit.Length + " i " + i);
		if ( OHit.Length > 0 ) {
			audio.clip = OHit[i];
			audio.Play ();
		}
		
	}

}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;
	public AudioClip[] attackFAIL;

	public AudioClip killed;

	public void PlayAttackOK()
	{
		audio.clip = attackOK;
		audio.Play ();
	}
	
	public void PlayAttackFAIL()
	{
		audio.clip = attackFAIL[Random.Range(0, attackFAIL.Length)];
		audio.Play ();
	}
	
	public void PlayKilled()
	{
		audio.clip = killed;
		audio.Play ();		
	}

}

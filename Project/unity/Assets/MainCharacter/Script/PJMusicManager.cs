using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;
	public AudioClip[] attackFAIL;

	public AudioClip killed;

	public static void PlayAttackOK()
	{
		audio.clip = attackOK;
		audio.Play ();
	}
	
	public static void PlayAttackFAIL()
	{
		audio.clip = attackFAIL[Random.Range(0, attackFAIL.GetLength)];
		audio.Play ();
	}
	
	public static void PlayKilled()
	{
		audio.clip = killed;
		audio.Play ();		
	}

}

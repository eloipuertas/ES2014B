using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class PJMusicManager : MonoBehaviour 
{
	public AudioClip attackOK;
	public AudioClip[] attackFAIL;

	public AudioClip girl_hurt;
	public AudioClip[] man_hurt;
	
	public AudioClip[] walkSounds;
	
	public AudioClip killed;

	public AudioClip potion;
	
	public void PlayAttackOK()
	{
		if (!audio.isPlaying) {
			audio.loop = false;
			audio.clip = attackOK;
			audio.Play ();
		}
	}
	
	public void PlayAttackFAIL()
	{
		audio.loop = false;
		audio.clip = attackFAIL[Random.Range(0, attackFAIL.Length)];
		audio.Play ();
	}

	public void PlayGirlHurt()
	{
		audio.loop = false;
		audio.clip = girl_hurt;
		audio.Play ();
	}

	public void PlayHurt()
	{
		audio.loop = false;
		string player = PlayerPrefs.GetString ("player");
		if (player.Equals("player1")) {
			audio.clip = girl_hurt;
		} else {
			audio.clip = man_hurt[Random.Range(0, man_hurt.Length)];
		}
		audio.Play ();
	}
	
	public void PlayKilled()
	{
		if (!audio.isPlaying) {
			audio.loop = false;
			audio.clip = killed;
			audio.Play ();	
		}
	}
	
	public void PlayWalkSounds()
	{
		if(!audio.isPlaying){
			audio.loop = true;
			audio.clip = walkSounds[Random.Range(0, walkSounds.Length)];;
			audio.Play ();
		}
	}
	
	public void StopWalkSounds()
	{
		if (audio.clip == walkSounds [0] || audio.clip == walkSounds [1] || audio.clip == walkSounds [2] || audio.clip == walkSounds [3]) {
			audio.loop = false;
			audio.Stop ();	
		}
	}
	
	public void DrinkPotion()
	{
		audio.loop = false;
		audio.clip = potion;
		audio.Play ();	
	}
	
}

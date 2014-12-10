using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AmbientalMusic : MonoBehaviour 
{
	public AudioClip catacumba;
	public AudioClip fight;

	public AudioClip gameOver;

	public AudioClip youWin;
	public AudioClip credits;

	public bool pause = false;

	// Use this for initialization
	void Start () 
	{
		audio.clip = catacumba;
		audio.loop = true;

		audio.Play ();
	
	}

	public void PlayGameOver()
	{
		audio.clip = gameOver;
		audio.Play ();
	}

	public void PlayFight()
	{
		audio.clip = fight;
		audio.Play ();
	}

	public void PlayCatacumba()
	{
		audio.clip = catacumba;
		audio.Play ();
	}

	public void PlayYouWin()
	{
		audio.clip = youWin;
		audio.Play ();
	}
	
	public void PlayCredits()
	{
		audio.clip = credits;
		audio.Play ();
	}

	public void PauseAudio()
	{
		audio.Pause ();
	}

	public void UnPauseAudio()
	{
		audio.Play ();
	}

}

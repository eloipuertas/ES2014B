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
	
	// Update is called once per frame
	void Update () 
	{
		if ( pause )
			audio.Pause ();
		
		else if ( ! audio.isPlaying )
				audio.Play ();
	
	}

	public void PlayGameOver()
	{
		audio.clip = gameOver;
	}

	public void PlayFight()
	{
		audio.clip = fight;
	}

	public void PlayCatacumba()
	{
		audio.clip = catacumba;
	}

	public void PlayYouWin()
	{
		audio.clip = youWin;
	}
	
	public void PlayCredits()
	{
		audio.clip = credits;
	}

	public void PauseAudio()
	{
		pause = true;
	}

	public void UnPauseAudio()
	{
		pause = false;
	}

}

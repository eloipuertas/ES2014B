using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AudioButtons : MonoBehaviour 
{
	public AudioClip level_easy;
	public AudioClip level_medium;
	public AudioClip level_hard;

	public AudioClip laughMan;
	public AudioClip laughGirl;

	void Start () 
	{
		audio.loop = false;
		
	}
	
	public void PlayLevelEasy()
	{
		audio.loop = false;
		audio.clip = level_easy;
		audio.Play();
	}
	
	public void PlayLevelMedium()
	{
		audio.loop = false;
		audio.clip = level_medium;
		audio.Play();
	}
	
	public void PlayLevelHard()
	{
		audio.loop = false;
		audio.clip = level_hard;
		audio.Play();
	}
	
	public void PlayLaughMan()
	{
		audio.loop = false;
		audio.clip = laughMan;
		audio.Play();
	}
	
	public void PlayLaughGirl()
	{
		audio.loop = false;
		audio.clip = laughGirl;
		audio.Play();
	}
	
}
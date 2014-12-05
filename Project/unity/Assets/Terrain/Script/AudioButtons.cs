using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AudioButtons : MonoBehaviour 
{
	public AudioSource audio;
	public AudioClip level_easy;
	public AudioClip level_medium;
	public AudioClip level_hard;

	void Start () 
	{
		audio.loop = false;
		
	}
	
	public void PlayLevelEasy()
	{
		audio.clip = level_easy;
		audio.Play();
	}
	
	public void PlayLevelMedium()
	{
		audio.clip = level_medium;
		audio.Play();
	}
	
	public void PlayLevelHard()
	{
		audio.clip = level_hard;
		audio.Play();
	}
	
}
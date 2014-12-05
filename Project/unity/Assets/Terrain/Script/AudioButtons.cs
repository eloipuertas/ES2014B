using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class AudioButtons : MonoBehaviour 
{
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
		audio.play();
	}
	
	public void PlayLevelMedium()
	{
		audio.clip = level_medium;
		audio.play();
	}
	
	public void PlayLevelHard()
	{
		audio.clip = level_hard;
		audio.play();
	}
	
}
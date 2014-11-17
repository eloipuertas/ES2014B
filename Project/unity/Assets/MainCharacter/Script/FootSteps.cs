using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( AudioSource ) ) ]

public class FootSteps : MonoBehaviour 
{
	public GameObject target;

	public AudioClip[] walkSounds;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( target.transform.hasChanged && ! audio.isPlaying ) 
		{
			audio.clip = walkSounds[Random.Range(0, walkSounds.Length)];
			audio.Play();
			
			target.transform.hasChanged = false;
			
		}
	
	}
}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class ParticleSystem : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		particleSystem.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.A))
			particleSystem.Play ();
	
	}
}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class Particules : MonoBehaviour 
{

	public ParticleSystem Mage;

	// Use this for initialization
	void Start () 
	{
		Mage.enableEmission = true;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Input.GetKeyDown ( KeyCode.A ) )
			Mage.Play();
	
	}
}

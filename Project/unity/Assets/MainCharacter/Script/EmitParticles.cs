﻿using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class EmitParticles : MonoBehaviour 
{
	Vector3 pos = new Vector3 ( 0, 15, 0 );
	Vector3 vel = new Vector3 ( 100, 0, 0 );
	float siz = 5;
	float lif = 3;
	Color32 col = Color.cyan;



	// Use this for initialization
	void Start () 
	{
		particleSystem.playOnAwake = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Input.GetKeyDown ( KeyCode.A ) ) 
		{
			particleSystem.Emit ( pos, vel, siz, lif, col );
		}

	}
}

using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]
[ RequireComponent( typeof( AudioSource ) ) ]

public class EmitParticles : MonoBehaviour 
{
	Vector3 pos = new Vector3 ( 0, 13, 0 );
	Vector3 vel = new Vector3 ( 0, 0, 70 );
	float siz = 3;
	float lif = 5;
	Color32 col = Color.white;

	public AudioClip[] fire_ball;
	
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

			audio.clip = fire_ball[Random.Range(0, fire_ball.Length)];
			audio.Play ();

		}

	}
}

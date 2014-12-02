using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]
[ RequireComponent( typeof( AudioSource ) ) ]

public class EmitParticles : MonoBehaviour 
{
	public Vector3 pos = new Vector3 ( 0, 13, 0 ); // Setejar a la posicio de la ma

	private Vector3 vel = new Vector3();

	public int velocitat = 2;

	public float siz = 3;
	public float lif = 5;
	public float velx, vely, velz;
	public float normal;

	public Color32 col = Color.white;
	
	public AudioClip fire_ball;

	public void throwParticle ( GameObject player, Vector3 target ) 
	{
		// Definim origen de la particula
		transform.position = player.transform.position + pos;
		transform.LookAt (target);

		if(!particleSystem.isPlaying)
			particleSystem.Emit (1);
		
		audio.clip = fire_ball;
		audio.Play ();

	}
	
}


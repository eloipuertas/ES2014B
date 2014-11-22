using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class EmitParticles : MonoBehaviour 
{
	public Vector3 pos = new Vector3 ( 0, 13, 0 ); // Setejar a la posicio de la ma
	public Vector3 vel = new Vector3();

	public int velocitat = 1;

	public float siz = 3;
	public float lif = 5;

	public Color32 col = Color.white;
	
	public AudioClip fire_ball;

	GameObject player = GameObject.FindWithTag("Player");
	GameObject target = GameObject.FindWithTag("PNJ");
	
	// public void throwParticle( GameObject player, GameObject target ) 
	public void throwParticle()
	{		
		// Definim origen de la particula
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		
		// Definim la velocitat ( direccio de la bola ) a partir del target
		vel [0] = (target.transform.position.x - player.transform.position.x) * velocitat;
		vel [1] = (target.transform.position.y - player.transform.position.y) * velocitat;
		vel [2] = (target.transform.position.z - player.transform.position.z) * velocitat;
		
		if( ! particleSystem.isPlaying )
			particleSystem.Emit ( pos, vel, siz, lif, col );
		
		audio.clip = fire_ball;
		audio.Play ();

	}
	
}


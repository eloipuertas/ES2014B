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
		//transform.rotation = player.transform.rotation;
		transform.LookAt (target);
		//transform.localScale.z = target.magnitude;

		// Definim la velocitat ( direccio de la bola ) a partir del target

		//vel [0] =  velocitat;
		//vel [1] =  velocitat;
		//vel [2] = velocitat;


		//vel = Vector3.Lerp(player.transform.position, target.transform.position, 0.1f);
		
		// Definim vector director

		/*velx = (target.transform.position.x - player.transform.position.x);
		vely = (target.transform.position.y - player.transform.position.y);
		velz = (target.transform.position.z - player.transform.position.z);*/
		//vel = target - player.transform.position;

		// Normalitzem vector director i donem velocitat

		//normal = Mathf.Sqrt( velx *velx +vely *vely +velz *velz );

		/*
		 * var distance = heading.magnitude;
			var direction = heading / distance;
		 */
		//float distance = vel.magnitude;
		//vel = (vel / distance) * velocitat;

		/*velx = velx /normal * velocitat;
		vely = vely /normal * velocitat;
		velz = velz /normal * velocitat;

		vel = new Vector3 ( velx, vely, velz );*/

		
		if(!particleSystem.isPlaying)
			particleSystem.Emit (1);
		
		audio.clip = fire_ball;
		audio.Play ();

	}
	
}


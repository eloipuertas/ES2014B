using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class BitParticles : MonoBehaviour 
{

	public void SpiderBit ( GameObject spider ) 
	{
		// Definim origen de la particula
		transform.position = spider.transform.position;
		transform.rotation = spider.transform.rotation;

		if(!particleSystem.isPlaying)
			particleSystem.Emit ( 5 );

	}
	
}


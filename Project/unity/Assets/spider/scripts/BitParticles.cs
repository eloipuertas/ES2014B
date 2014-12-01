using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( ParticleSystem ) ) ]

public class BitParticles : MonoBehaviour 
{

	public void SpiderBit ( Vector3 pos ) 
	{
		// Definim origen de la particula
		transform.position = pos + new Vector3(0,7,0);
		transform.LookAt (pos);

		particleSystem.Emit ( 15 );

	}
	
}


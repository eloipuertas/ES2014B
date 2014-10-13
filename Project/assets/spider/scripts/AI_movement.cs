using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour {
	public float speed;
	public Transform object_to_follow;
	private Transform me;

	// Use this for initialization
	void Start () {
		me = transform;
	}
	
	// Update is called once per frame
	void Update () {
		float distX = object_to_follow.position.x-me.position.x;
		float distZ = object_to_follow.position.z-me.position.z;
	    float dist = (Mathf.Sqrt (Mathf.Pow(distX,2)+Mathf.Pow(distZ,2)));
		float prop = speed/dist;

		Vector3 velocity = new Vector3(prop*distX, 0, prop*distZ);

		rigidbody.velocity = velocity;
	}
}

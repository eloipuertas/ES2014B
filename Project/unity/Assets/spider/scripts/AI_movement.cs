using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour {
	public Transform target;
	NPCState myState;

	// Use this for initialization
	void Start () {

	}
	void awake () {
		myState = this.gameObject.GetComponent<NPCState> ();
		myState.setDestination (target.position.x, 0, target.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		myState = this.gameObject.GetComponent<NPCState> ();

		if (Vector3.Distance (transform.position,target.position)>2) {
			myState.setDestination (target.position.x, target.position.y, target.position.z);
		} else {
			myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
		}
	}
}

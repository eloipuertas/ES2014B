using UnityEngine;
using System;

public class AI_movement : MonoBehaviour {
	public Transform target;
	public float aggro_range = 3.5f;
	public float attack_range = 1.5f; //TODO move to NPCState
	private NPCState myState;

	private const int PASSIVE = 0;
	private const int MOVING = 1;
	private const int ATTACKING = 2;
	private int current_action = PASSIVE;
		
	void Start () {

	}
	void awake () {
		myState = this.gameObject.GetComponent<NPCState> ();

		GameObject goTarget = GameObject.FindWithTag("player");

		target = goTarget.transform;

		if (target != null) {
			myState.setDestination (target.position.x, 0, target.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		myState = this.gameObject.GetComponent<NPCState> ();

		if (target == null) {
			GameObject goTarget = GameObject.FindWithTag("player");
			target = goTarget.transform;
		}

		if ( target != null ) {
			// TODO Before calculating distance, check if the target is in VISUAL range!
			float dist = Vector3.Distance (transform.position, target.position);

			if (myState.state!=NPCState.MUERTO){
				if (dist<attack_range){
					if (current_action==MOVING){
						myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
					}
					NPCState targetState = target.GetComponent<NPCState>();
					if ( targetState != null && targetState is IAttacker ) {
						myState.attack(targetState); //TODO Swap to whatever implements the interface on the main character when integrating to devel
						current_action = ATTACKING;
					}
				}else if (dist<aggro_range){
					if (current_action != MOVING){
						current_action = MOVING;
					}
					myState.setDestination (target.position.x, target.position.y, target.position.z);
				}
				//If we add any blink skills on the player character, this AI would be completely screwed over...
			}else if (current_action == MOVING){
				myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
				current_action = PASSIVE;
			}
		}
	}
}

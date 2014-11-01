using UnityEngine;
using System;

public class SpiderAI : MonoBehaviour {
	public Transform target;
	public float aggroRange = 3.5f;
	public float attackRange = 1.5f; //TODO move to NPCState
	private SpiderState myState;

	private const int PASSIVE = 0;
	private const int MOVING = 1;
	private const int ATTACKING = 2;
	public int currentAction = MOVING;
		
	void Start () {

	}
	void awake () {
		myState = this.gameObject.GetComponent<SpiderState> ();

		GameObject goTarget = getPlayerGameObject();

		target = goTarget.transform;

		if (target != null) {
			myState.setDestination (target.position.x, target.position.y, target.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		myState = this.gameObject.GetComponent<SpiderState> ();

		if (target == null) {
			GameObject goTarget = getPlayerGameObject();
			target = goTarget.transform;
		}

		if ( target != null ) {
			// TODO Before calculating distance, check if the target is in VISUAL range!
			float dist = Vector3.Distance (transform.position, target.position);
			//Debug.Log("dist: " + dist, gameObject);

			if (myState.isAlive()){
				if (dist<attackRange){
					if (currentAction==MOVING){
						myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
					}
					AbstractEntity targetEntity = target.GetComponent<AbstractEntity>();
					if ( targetEntity != null ) {
						myState.attack(targetEntity); //TODO Swap to whatever implements the interface on the main character when integrating to devel
						currentAction = ATTACKING;
					}
				}else if (dist<aggroRange){
					if (currentAction != MOVING){
						currentAction = MOVING;
					}
					myState.setDestination (target.position.x, target.position.y, target.position.z);
				}
			}else if (currentAction == MOVING){
				myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
				currentAction = PASSIVE;
			}
		}
	}

	private GameObject getPlayerGameObject() {
		return GameObject.FindWithTag("Player");
    }
}

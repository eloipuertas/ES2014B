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
	public int currentAction = PASSIVE;
		
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
		float dist;
		if ( target != null ) {
			dist = Vector3.Distance (transform.position, target.position);

			if (myState.isAlive()){
				if (dist<attackRange){
					if (currentAction==MOVING){
						myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
					}
					AbstractEntity targetEntity = target.GetComponent<AbstractEntity>();
					if ( targetEntity != null ) {
						myState.attack(targetEntity); 
						currentAction = ATTACKING;
					}
				}else if (dist<aggroRange){
					if (currentAction == PASSIVE){
						RaycastHit hit;
						if(Physics.Raycast(transform.position, target.position-transform.position, out hit, dist)) {
							if ((hit.point-target.position).magnitude<1){ //TODO to change when the main character fixes their tag
								currentAction = MOVING;
								myState.setDestination (target.position.x, target.position.y, target.position.z);
							}
						}
					}else{
						if (currentAction == ATTACKING) currentAction = MOVING;
						myState.setDestination (target.position.x, target.position.y, target.position.z);
					}
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

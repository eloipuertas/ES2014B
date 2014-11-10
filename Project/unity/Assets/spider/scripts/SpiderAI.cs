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
	private int currentAction = PASSIVE;

	private Vector3 lastTargetPos;
	private NavMeshAgent agent;
	private NavMeshPath path;
	private int current_corner = 1;
		
	void Start () {

	}
	void Awake () {
		myState = this.gameObject.GetComponent<SpiderState> ();
		agent = GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;

		if (target == null) {
			GameObject goTarget = getPlayerGameObject ();
			target = goTarget.transform;
		}

		path = new NavMeshPath ();
		lastTargetPos = target.position;
		InvokeRepeating ("checkPath", 0, 0.25f);
	}

	private void checkPath(){
		if ((!Vector3.Equals (lastTargetPos, target.position)||target.tag!="Player") && currentAction==MOVING) {
			agent.CalculatePath(target.position, path);
			lastTargetPos = target.position;
			current_corner = 1;
			/**
			Debug.Log ("===========================");
			Debug.Log ("My position: " +transform.position);
			Debug.Log ("---------------------------");
			for (int i=0;i<path.corners.Length;i++){
				if (i==current_corner) Debug.Log (">>>>>"+path.corners[i]);
				else Debug.Log (path.corners[i]);
			}
			Debug.Log ("---------------------------");
			Debug.Log ("Enemy Position: " +target.position);
			Debug.Log ("===========================");
			**/
		}
	}

	// Update is called once per frame
	void Update () {
		if (target == null) {
			GameObject goTarget = getPlayerGameObject();
			target = goTarget.transform;
		}
		float dist;
		if ( target != null ) {
			if (myState.isAlive()){
				dist = Vector3.Distance (transform.position, target.position);
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
					if (path.corners.Length>0){
						if (Vector3.Equals(path.corners[current_corner],transform.position)){
							current_corner++;
						}
						Vector3 dest = path.corners[current_corner];
						myState.setDestination (dest.x, dest.y, dest.z);
					}
					if (currentAction == PASSIVE){
						RaycastHit hit;
						if(Physics.Raycast(transform.position, target.position-transform.position, out hit, dist)) {
							if ((hit.point-target.position).magnitude<1){ //TODO to change when the main character fixes their tag
								currentAction = MOVING;
							}
						}
					}else if (currentAction == ATTACKING) currentAction = MOVING;
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

using UnityEngine;
using System;

public class WebSpiderAI : BasicAI {

	public float aggroRange = 75f;
	public float spellRange;
	public float attackRange = 10f; 
	public int web_manacost = 100;
	private SpiderState myState;
	
	private Vector3 lastTargetPos;
	private NavMeshAgent agent;
	private NavMeshPath path;
	private int current_corner;
	
	void Start () {
		
	}
	void Awake () {
		myState = this.gameObject.GetComponent<SpiderState> ();
		agent = GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;

		if (target == null) {
			GameObject goTarget = getPlayerGameObject ();
			if ( goTarget != null ) {
				target = goTarget.transform;
			}
		}
		path = new NavMeshPath ();
		/* if ( target != null ) {
			agent.CalculatePath(target.position, path);
			lastTargetPos = target.position;
		} */
		current_corner = 1;
		spellRange = attackRange * 5f;
		InvokeRepeating ("checkPath", 0, 0.25f);
	}
	
	private void checkPath(){
		if (!Vector3.Equals (lastTargetPos, target.position) && currentAction==MOVING) {
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
	
	// DebugRays are only visible in SCENE view, not in GAME view!
	private void debugDrawPath()
	{
		Vector3[] corners = path.corners;
		
		if ( corners.Length >= 2 ) {
			//Debug.Log("Drawing path, corners.Length: " + corners.Length);
			
			for ( int i = 1 ; i < corners.Length ; i++ ) {
				//while ( i < corners.Length ) {
				Vector3 u = corners[i - 1];
				Vector3 v = corners[i];
				// UnityEngine.Debug.DrawRay(UnityEngine.Vector3, UnityEngine.Vector3, UnityEngine.Color, float, bool)
				
				if ( i % 2 != 0 ) {
					Debug.DrawRay(v, u, Color.green, 100, true);
				} else {
					Debug.DrawRay(u, v, Color.green, 100, true);
				}
				//Debug.Log("i: " + i + ", u: " + u + ", v: " + v);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//debugDrawPath();
		if (target == null) {
			GameObject goTarget = getPlayerGameObject ();
			if ( goTarget != null ) {
				target = goTarget.transform;
			}
		}
		if ( target != null && Time.timeScale>0 ) {
			AbstractEntity targetEntity = target.GetComponent<AbstractEntity>();
			if ( targetEntity != null) {
				if (myState.isAlive() && targetEntity.isAlive ()){
					float dist = Vector3.Distance (transform.position, target.position);
					if (dist<attackRange){
						if (currentAction==MOVING){
							myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
						}
						myState.attack(targetEntity,target.position); 
						currentAction = ATTACKING;

					}else if (dist<spellRange && myState.getMP ()>web_manacost){
						myState.throwProj (targetEntity,target.position,web_manacost);
					}else if (dist<aggroRange){
						if (currentAction == PASSIVE){
							RaycastHit hit;
							if(Physics.Raycast(transform.position, target.position-transform.position, out hit, dist)) {
								if ((hit.point-target.position).magnitude<1){ //TODO to change when the main character fixes their tag
									currentAction = MOVING;
									checkPath ();
									if ( current_corner < path.corners.Length ) {
										if (Vector3.Equals(path.corners[current_corner],transform.position)){
											current_corner++;
										}
										Vector3 dest = path.corners[current_corner];
										myState.setDestination (dest.x, dest.y, dest.z);
									}
								}
							}
						}else if (currentAction == ATTACKING){
							currentAction = MOVING;
							checkPath ();
							if ( current_corner < path.corners.Length ) {
								if ( Vector3.Equals(path.corners[current_corner],transform.position) ){
									current_corner++;
								}
								Vector3 dest = path.corners[current_corner];
								myState.setDestination (dest.x, dest.y, dest.z);
							}
						}else{
							if ( current_corner < path.corners.Length ) {
								if ( Vector3.Equals(path.corners[current_corner],transform.position) ){
									current_corner++;
								}
								Vector3 dest = path.corners[current_corner];
								myState.setDestination (dest.x, dest.y, dest.z);
							}
						}
					}
				}else if (currentAction != PASSIVE){
					myState.setDestination (transform.position.x, transform.position.y, transform.position.z);
					currentAction = PASSIVE;
				}
			}
		}
	}
	
	private GameObject getPlayerGameObject() {
		return GameObject.FindWithTag("Player");
	}
}
using UnityEngine;
using System.Collections;

public class TrollAI : BasicAI {
	public float aggroRange = 75f;
	public float attackRange = 10f; 
	private TrollState myState;

	private NavMeshAgent agent;
	
	void Awake(){
		myState = this.gameObject.GetComponent<TrollState> ();
		agent = this.GetComponent<NavMeshAgent>();
		if (target == null) {
			GameObject goTarget = getPlayerGameObject ();
			if ( goTarget != null ) {
				target = goTarget.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
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
							//agent.Stop ();
						}
						myState.attack(targetEntity,target.position); 
						currentAction = ATTACKING;
						
					}else if (dist<aggroRange){
						if (currentAction == PASSIVE){
							RaycastHit hit;
							if(Physics.Raycast(transform.position, target.position-transform.position, out hit, dist)) {
								if ((hit.point-target.position).magnitude<1){ //TODO to change when the main character fixes their tag
									currentAction = MOVING;
									//agent.SetDestination(target.position);
								}
							}
						}else if (currentAction == ATTACKING){
							currentAction = MOVING;
							//agent.SetDestination(target.position);
						}else{
							//agent.SetDestination(target.position);
						}
					}
				}else if (currentAction != PASSIVE){
					//agent.Stop();
					currentAction = PASSIVE;
				}
			}
		}
	}

	private GameObject getPlayerGameObject() {
		return GameObject.FindWithTag("Player");
	}
}

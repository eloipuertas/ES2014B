using UnityEngine;
using System.Collections;

public class SpiderState : AbstractEntity {

	public float moveSpeed = 1.5f;
	public float rotationSpeed = 4.0f;

	public Vector3 destination;

	CharacterController characterController;
	Animator animator;
	
	void Awake(){
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		//TODO fix attributes using D&D formulaes on the base stats
		HP = 100;
		DMG = 5;

		setDestination(transform.position.x,transform.position.y,transform.position.z);
		
		/*// Get the Animator component from your gameObject
		// Sets the value
		animator.SetBool("InConga", true); 
		// Gets the value
		bool isInConga = animator.GetBool("InConga");*/

	}
	
	void Update(){
		if(this.isAlive()){
			move();
		}
	}

	public override void onAttackReceived (int baseDMG)
	{
		this.substractHealth(baseDMG);
	}

	private void move(){
		if ( animator != null && characterController != null ) { 
			Vector3 moveDirection = destination-transform.position;
			moveDirection.Normalize();
			moveDirection *= moveSpeed;
			if(moveDirection.magnitude < 0.5){
				animator.SetBool("walk_enabled",false);
			}
			characterController.Move (moveDirection * Time.deltaTime);
			this.lookAt();
		}
	}
	
	// LOOK
	public void lookAt(){
		if((destination - transform.position).magnitude < 0.1){
			return;
		}
		Quaternion newRotation = Quaternion.LookRotation(destination - transform.position);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,(rotationSpeed/1)*Time.deltaTime);
	}
	
	// ATTACK
	public void attack(AbstractEntity enemy){
		this.lookAt ();
		if (this.isAlive()) {
			animator.SetBool ("attack_enabled", true);
			enemy.onAttackReceived (DMG);
		} else {
			animator.SetBool("attack_enabled",false);
		}
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}

	public void setDestination(float x,float y,float z){
		if (animator != null) {
			if (isAlive ()) {
				animator.SetBool ("attack_enabled", false);
				animator.SetBool ("walk_enabled", true);
				destination = new Vector3 (x, y, z);
			} else {
				animator.SetBool ("attack_enabled", false);
				animator.SetBool ("walk_enabled", false);
			}
		}
	}
	
	// HP
	public void setHealth(int newHealth){
		if(isAlive()){
			HP = newHealth;
			if(HP <= 0){
				HP = 0;
			}
		}
	}

	public void addHealth(int healthToAdd){
		HP = HP + healthToAdd;
	}
	public void substractHealth(int healthToSubstract){
		HP = HP - healthToSubstract;
	}
}
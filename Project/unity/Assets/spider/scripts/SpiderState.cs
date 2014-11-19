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
		
		characterController.radius = 2.5f;
		
		//TODO fix attributes using D&D formulaes on the base stats
		setMAXHP(100);
		setMAXMP(100);
		setHP(75);
		setMP(50);
		DMG = 10;
		
		setDestination(transform.position.x,transform.position.y,transform.position.z);
		
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
			this.lookAt(destination);
		}
	}
	
	// LOOK
	public void lookAt(Vector3 lookAtPos){
		if(!Vector3.Equals(lookAtPos, transform.position)){
			Quaternion newRotation = Quaternion.LookRotation(lookAtPos - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, (rotationSpeed / 1) * Time.deltaTime);
		}
	}
	
	// ATTACK
	public void attack(AbstractEntity enemy, Vector3 enemyPos){
		this.lookAt(enemyPos);
		if(this.isAlive()){
			animator.SetBool ("attack_enabled", true);
			enemy.onAttackReceived (DMG);
		}else{
			animator.SetBool("attack_enabled",false);
		}
	}
	
	// THROW PROJECTILE
	public bool throwProj(AbstractEntity enemy,Vector3 enemyPos){
		// TODO
		return false;
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
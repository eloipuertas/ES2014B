using UnityEngine;
using System.Collections;

public class NPCState : EntityState {
	
	public float moveSpeed = 1.5f;
	public float rotationSpeed = 4.0f;
	public int maxHealth = 100;
	public int health = 100;
	public int damageAttack = 5;

	public Vector3 destination;

	CharacterController characterController;
	Animator animator;
	
	void Awake(){
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		
		setDestination(transform.position.x,transform.position.y,transform.position.z);
		state = VIVO;
		
		/*// Get the Animator component from your gameObject
		// Sets the value
		animator.SetBool("InConga", true); 
		// Gets the value
		bool isInConga = animator.GetBool("InConga");*/

	}
	
	void Update(){
		if(state != MUERTO){
			move();
		}
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
			lookAt();
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
	public new int attack(IAttacker attacker){
		lookAt ();
		if (state != MUERTO && attacker.getState () != INATACABLE) {
			state = ATACANDO;
			animator.SetBool ("attack_enabled", true);
			attacker.receiveDamage (damageAttack);
		} else {
			animator.SetBool("attack_enabled",false);
		}
		return state;
	}
	
	public new int receiveDamage(int damage){
		substractHealth(damage);
		return state;
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}
	public void setDestination(float x,float y,float z){
		if (animator != null) {
			if (state != MUERTO) {
				animator.SetBool ("attack_enabled", false);
				animator.SetBool ("walk_enabled", true);
				state = VIVO;
				destination = new Vector3 (x, y, z);
			} else {
				animator.SetBool ("attack_enabled", false);
				animator.SetBool ("walk_enabled", false);
			}
		}
	}
	
	// HP
	public void setHealth(int newHealth){
		if(state != MUERTO){
			health = newHealth;
			if(health <= 0){
				health = 0;
				state = MUERTO;
			}
		}
	}
	public int getHealth(){
		return health;
	}
	public void setMaxHealth(int newMaxHealth){
		maxHealth = newMaxHealth;
	}
	public int getMaxHealth(){
		return maxHealth;
	}
	public void addHealth(int healthToAdd){
		setHealth(health+healthToAdd);
	}
	public void substractHealth(int healthToSubstract){
		setHealth(health-healthToSubstract);
	}
}
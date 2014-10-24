using UnityEngine;
using System.Collections;

public class NPCState : MonoBehaviour, IAttacker {
	
	public static int MUERTO = 0;
	public static int VIVO = 1;
	public static int ATACANDO = 2;
	public static int INATACABLE = 3;
	public static int OTRO = 4;
	
	public float moveSpeed = 1.5f;
	public int maxHealth = 100;
	public int health = 100;
	public int damageAttack = 5;
	public int state = 1;
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
		Vector3 moveDirection = destination-transform.position;
		moveDirection.Normalize();
		moveDirection *= moveSpeed;
		if(moveDirection.magnitude < 0.5){
			animator.SetBool("walk_enabled",false);
		}
		characterController.Move (moveDirection * Time.deltaTime);
		lookAt();
	}
	
	// LOOK
	public void lookAt(){
		if((destination - transform.position).magnitude < 0.1){
			return;
		}
		Quaternion newRotation = Quaternion.LookRotation(destination - transform.position);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,(moveSpeed/1)*Time.deltaTime);
	}
	
	// ATTACK
	int IAttacker.attack(IAttacker attacker){
		if (state != MUERTO && attacker.getState () != INATACABLE) {
			state = ATACANDO;
			animator.SetBool ("attack_enabled", true);
			attacker.receiveDamage (damageAttack);
		} else {
			animator.SetBool("attack_enabled",false);
		}
		return state;
	}
	
	int IAttacker.receiveDamage(int damage){
		substractHealth(damage);
		return state;
	}
	
	int IAttacker.getState(){
		return state;
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}
	public void setDestination(float x,float y,float z){
		if (state != MUERTO) {
				animator.SetBool ("attack_enabled", false);
				animator.SetBool ("walk_enabled", true);
				state = VIVO;
				destination = new Vector3 (x, y, z);
		} else {
			animator.SetBool("attack_enabled",false);
			animator.SetBool("walk_enabled",false);
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
	
	public int getState(){
		return state;
	}
}

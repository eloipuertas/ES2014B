using UnityEngine;
using System.Collections;

public class NPCState : MonoBehaviour {
	
	public float moveVelocity = 10f;
	public int maxHealth = 100;
	public int health = 100;
	public Vector3 destination;
	
	void Awake(){
		destination = transform.position;
		//destination = new Vector3(100,100,100);
		//animation = gameObject.animation;
		//gameObject.animation.Play("walk",PlayMode.StopAll);
		/*if(!animation.isPlaying){
		}*/
	}
	
	void Update(){
		moveToDestination();
	}
	
	public void moveToDestination(){
		Vector3 direction = destination-transform.position;
		if(direction.magnitude > 1){
			direction.Normalize();
			//rigidbody.velocity = direction*moveVelocity*Time.deltaTime;
			rigidbody.velocity = direction - transform.position;
		}else{
			transform.position = destination;
		}
	}
	
	public Vector3 getDestination(){
		return destination;
	}
	
	public void setDestination(float x,float y,float z){
		destination = new Vector3(x,y,z);
	}
	
	public void setHealth(int newHealth){
		health = newHealth;
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

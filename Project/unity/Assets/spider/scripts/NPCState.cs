using UnityEngine;
using System.Collections;

public class NPCState : MonoBehaviour {
	
	
	public float moveVelocity = 5f;
	public int maxHealth = 100;
	public int health = 100;
	private Vector3 direction;
	private Vector3 destination;
	
	// Use this for initialization
	void Start(){
		direction = new Vector3(0,0,0);
		destination = transform.position;	// stand still
	}
	
	// Update is called once per frame
	void Update(){
		direction = destination-transform.position;
		if(direction.magnitude > 1){
			direction.Normalize();
			Vector3 velocity = direction*moveVelocity*Time.deltaTime;
			transform.Translate(velocity);
		}else{
			transform.Translate(direction);
		}
		
		if(!animation.isPlaying){
			animation.Play();
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

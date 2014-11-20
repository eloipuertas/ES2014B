using UnityEngine;
using System.Collections;


public class SpiderState : AbstractEntity {
	public float moveSpeed = 1.5f;
	public float rotationSpeed = 4.0f;
	public Vector3 destination;
	public float timecost_perAction;
	
	private CharacterController characterController;
	private Animator animator;
	private float timeForNextAction = 0;
	
	void Awake(){
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		
		characterController.radius = 2.5f;

		if (STR == 0) setSTR (6);
		else if (STR < 0) setSTR (1);
		else if (STR > 18) setSTR (18);
		if (DEX == 0) setDEX (6);
		else if (DEX < 0) setDEX (1);
		else if (DEX > 18) setDEX (18);
		if (CON == 0) setCON (2);
		else if (CON < 0) setCON (1);
		else if (CON > 18) setCON (18);
		if (INT == 0) setINT (6);
		else if (INT < 0) setINT (1);

		if (HP == 0) setHP (Mathf.RoundToInt (((float)CON/18) * 500));

		if (MAXHP == 0) setMAXHP (HP);
		if (FOR == 0) setFOR (Mathf.RoundToInt ((float) CON * 0.25f));
		if (REF == 0) setREF (Mathf.RoundToInt ((float) DEX * 0.5f));
		if (ARM == 0) setARM (FOR+REF);

		if (MP == 0) setMP (Mathf.RoundToInt (((float)INT/18) * 500));
		if (MAXMP == 0) setMAXMP (MP);
		if (DMG == 0) setDMG ((int)((float) STR * 0.5));

		if (timecost_perAction == 0) timecost_perAction = 1/((float)DEX/18 * 20);

		setDestination(transform.position.x,transform.position.y,transform.position.z);
		InvokeRepeating ("TimeBasedUpdate", 0, 0.05f);
	}

	private void TimeBasedUpdate(){
		if (timeForNextAction > 0.0) timeForNextAction = timeForNextAction - 0.05f;
		if (MP < MAXMP) MP = MP + 1;
	}

	void Update(){
		if(this.isAlive()){
			move();
		}
	}
	
	public override void onAttackReceived (int baseDMG)
	{
		int damage = Mathf.RoundToInt((1-((float)ARM / 15 * 0.75f))*baseDMG);
		this.substractHealth(damage);
	}
	
	private void move(){
		if ( animator != null && characterController != null) { 
			Vector3 moveDirection = destination-transform.position;
			moveDirection.Normalize();
			moveDirection *= moveSpeed;
			if(moveDirection.magnitude < 0.5 && animator.GetBool("walk_enabled")){
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
		if(this.isAlive() && enemy.isAlive()){
			if (timeForNextAction<=0){
				if (!animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", true);
				enemy.onAttackReceived (DMG);
				timeForNextAction = timecost_perAction;
			}
		}else if (animator.GetBool("attack_enabled")){
			animator.SetBool("attack_enabled",false);
		}
	}
	
	// THROW PROJECTILE
	public void throwProj(AbstractEntity enemy,Vector3 enemyPos, int manacost){
			
		if (MP > manacost) {
			this.lookAt (enemyPos);
			MP = MP - manacost;
			GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			projectile.transform.position = new Vector3(transform.position.x,2,transform.position.z);
			projectile.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
			projectile.AddComponent<Web>();
			Rigidbody rgproj = projectile.AddComponent<Rigidbody>();
			rgproj.velocity = (enemyPos-projectile.transform.position).normalized*10;
			rgproj.useGravity = false;
		}
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}
	
	public void setDestination(float x,float y,float z){
		if (animator != null) {
			if (this.isAlive()) {
				if (animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", false);
				if (!animator.GetBool("walk_enabled")) animator.SetBool ("walk_enabled", true);
				destination = new Vector3 (x, y, z);
				if (timeForNextAction!=(timecost_perAction/2)) timeForNextAction = timecost_perAction/2;
			} else {
				if (animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", false);
				if (animator.GetBool("walk_enabled")) animator.SetBool ("walk_enabled", false);
			}
		}
	}
	
	// HP
	public void setHealth(int newHealth){
		if(isAlive()){
			setHP(newHealth);
			if(HP <= 0){
				setHP(0);
			}
		}
	}
	
	public void addHealth(int healthToAdd){
		setHP(HP + healthToAdd);
	}
	public void substractHealth(int healthToSubstract){
		setHP(HP - healthToSubstract);
	}
}
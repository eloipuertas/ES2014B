using UnityEngine;
using System.Collections;


public class SpiderState : AbstractEntity {
	public float timeForNextAction = 0;
	public float moveSpeed = 10f;
	public float rotationSpeed = 15f;
	public Vector3 destination;
	public float timecost_perAction;
	public float timeCostDivisor = 2;
	
	private PNJMusicManager PNJAudio;
	private CharacterController characterController;
	private Animator animator;
	private Transform enemy;
	
	public float projectileSpeed = 75f;
	public float max_attacks_per_second = 5; //Also means MP restored per second
	public int maxHPPossible = 500;
	public int maxMPPossible = 500;
	public float coeff_ConToFor = 0.25f;
	public float coeff_DexToRef = 0.5f;
	public float coeff_StrToDMG = 3f;
	public float maxPcDMGReduction = 0.75f;

	void Awake(){
		PNJAudio = GameObject.FindObjectOfType(typeof(PNJMusicManager)) as PNJMusicManager;
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		
		characterController.radius = 2.5f;
		
		updateStats ();

		InvokeRepeating ("TimeBasedUpdate", 0, 1f/max_attacks_per_second); 
	}

	public void updateStats(){
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

		setMAXHP (Mathf.RoundToInt (((float)CON/18f) * maxHPPossible));
		setHP (MAXHP);
		setFOR (Mathf.RoundToInt ((float) CON * coeff_ConToFor));
		setREF (Mathf.RoundToInt ((float) DEX * coeff_DexToRef));
		setARM (FOR+REF);

		setMAXMP (Mathf.RoundToInt (((float)INT/18f) * maxMPPossible));
		setMP (MAXMP);
		setDMG (Mathf.RoundToInt ((float) STR * coeff_StrToDMG));
		
		timecost_perAction = (1f/((float)DEX/18f * max_attacks_per_second));
	}

	private void TimeBasedUpdate(){ 
		if (timeForNextAction > 0.0) timeForNextAction = timeForNextAction - 1f/max_attacks_per_second;
		if (MP < MAXMP) MP = MP + 1;
	}
	
	void Update(){
		if(this.isAlive() && Time.timeScale>0 && !Vector3.Equals(destination,new Vector3(0,0,0))){
			move();
		}
	}
	
	public override void onAttackReceived (int baseDMG){
		//Debug.Log("SpiderState: onAttackReceived");
		int damage = Mathf.RoundToInt((1-((float) ARM / 15 * maxPcDMGReduction))*baseDMG);
		//Debug.Log("spider_baseDMG: " + baseDMG);
		Debug.Log("spider_damage: " + damage);
		if (animator.GetBool ("walk_enabled")) animator.SetBool("walk_enabled",false);
		if (animator.GetBool ("attack_enabled")) animator.SetBool("attack_enabled",false);
		if (animator.GetBool ("critical")) animator.SetBool("critical",false);
		if (!animator.GetBool ("receive_attack_enabled")) animator.SetBool("receive_attack_enabled",true);
		this.substractHealth(damage);
		if (timeCostDivisor > 0 && timeForNextAction<(timecost_perAction/timeCostDivisor)) timeForNextAction = timecost_perAction/timeCostDivisor;
	}
	
	private void move(){
		if (animator.GetBool ("receive_attack_enabled")) animator.SetBool("receive_attack_enabled",false);
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
				float randomNumber = Random.Range(0f,100f);
				if (randomNumber<25){
					if (animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", false);
					if (!animator.GetBool("critical")) animator.SetBool ("critical", true);
					PNJAudio.PlayCriticalAttack(); // PlayAttackOK
					enemy.onAttackReceived (4*DMG);
					timeForNextAction = timecost_perAction;
				}else{
					if (animator.GetBool("critical")) animator.SetBool ("critical", false);
					if (!animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", true);
					PNJAudio.PlayAttackOK(); // PlayCriticalAttack
					enemy.onAttackReceived (DMG);
					timeForNextAction = timecost_perAction;
				}
			}
		}else if (animator.GetBool("attack_enabled")){
			animator.SetBool("attack_enabled",false);
		}else if (animator.GetBool("critical")){
			animator.SetBool("critical",false);
		}
	}

	public void useWebSpell(Transform target){
		this.GetComponent<WebSpiderAI>().enabled = false;
		if (animator.GetBool("walk_enabled")) animator.SetBool("walk_enabled",false);
		if (!animator.GetBool("web")) animator.SetBool("web",true);
		destination = transform.position;
		enemy = target;
		this.lookAt (enemy.transform.position);
		Invoke ("reenableWebAI",1.5f);
		Invoke ("throwProj",1.25f);
	}

	private void throwProj(){
		if (this.isAlive ()) {
			setMP(MP - this.GetComponent<WebSpiderAI>().web_manacost);
			this.lookAt (enemy.transform.position);
			Object prefab = Resources.Load("SpiderWeb", typeof(GameObject));
			GameObject projectile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
			projectile.GetComponent<Web>().setTarget(enemy);
			//Physics.IgnoreCollision(projectile.collider,characterController);
			projectile.transform.position = new Vector3(transform.position.x,transform.position.y*1.5f,transform.position.z);
			projectile.transform.rotation = projectile.transform.rotation * Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
			Rigidbody rgproj = projectile.AddComponent<Rigidbody>();
			Vector3 moveDirection = enemy.transform.position-transform.position;
			rgproj.velocity = new Vector3(moveDirection.x,0,moveDirection.z).normalized * projectileSpeed;
			rgproj.useGravity = false;
			Physics.IgnoreCollision(rgproj.collider,characterController);
		}
	}

	private void reenableWebAI(){
		animator.SetBool("web",false);
		this.GetComponent<WebSpiderAI>().enabled = true;
	}
	
	// MOVEMENT
	public Vector3 getDestination(){
		return destination;
	}
	
	public void setDestination(float x,float y,float z){
		if (animator != null) {
			if (animator.GetBool("critical")) animator.SetBool ("critical", false);
			if (animator.GetBool("attack_enabled")) animator.SetBool ("attack_enabled", false);
			if (this.isAlive()) {
				if (!animator.GetBool("walk_enabled")) animator.SetBool ("walk_enabled", true);
				destination = new Vector3 (x, y, z);
			} else {
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
		if(!isAlive()){
			if (animator.GetBool("walk_enabled")) animator.SetBool("walk_enabled",false);
			if (animator.GetBool("attack_enabled")) animator.SetBool("attack_enabled",false);
			if (animator.GetBool("critical")) animator.SetBool("critical",false);
			if (animator.GetBool("receive_attack_enabled")) animator.SetBool("receive_attack_enabled",false);
			animator.SetBool("die",true);
			GetComponent<CharacterController>().enabled = false;
			PNJAudio.PlayPNJKilled();
		}
	}
	public void destroyWithDelay(float delay){
		Invoke ("destroyObject",delay);
	}
	public void destroyObject(){
		Destroy(transform.gameObject);
	}
}
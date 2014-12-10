using UnityEngine;
using System.Collections;


public class MainPjMovement :  AbstractEntity {
	private GameObject player;
	private RaycastHit hit;
	public float speed = 50;
	
	public int nextMagicAttack;
	public Vector3 targetPosition;
	private Vector3 lastPosition;
	
	private CharacterController controller;
	public float lerpMoving;
	public int i;
	
	private PJMusicManager PJAudio;
	public bool shield;
	private Animator anim;
	public bool paused;
	public float freeze;


	private EmitParticles Magia;
	private BitParticles ParticulesSang;

	public int physicalAttackRange = 60;

	public float manaSpellCost = 0.25f;
	
	public int regenHP;
	public int regenMP;
	private int nivellDif;
	private bool clickPressed = false;
	
	public bool leftPressedMouse(){
		return this.clickPressed;
	}
	public void modeEasy(){
		this.setMAXHP (750);
		this.setMAXMP (1000);
		this.setHP (750);
		this.setMP (1000);
		this.setFOR (0);
		this.setDMG (25);
		this.setRegenHP (15);
		this.setRegenMP (50);
		nivellDif = 1;
	}
	
	public void modeMedium(){
		this.setMAXHP (500);
		this.setMAXMP (500);
		this.setHP (500);
		this.setMP (500);
		this.setFOR (0);
		this.setDMG (15);
		this.setRegenHP (10);
		this.setRegenMP (25);
		nivellDif = 2;
	}
	public void modeHard(){
		this.setMAXHP (300);
		this.setMAXMP (400);
		this.setHP (300);
		this.setMP (400);
		this.setFOR (0);
		this.setDMG (15);
		this.setRegenHP (3);
		this.setRegenMP (10);
		nivellDif = 3;
	}

	public int getSelectedSpell(){
		return nextMagicAttack-1;
	}
	public void setShield(bool n){
		this.shield = n;
		this.setFOR(3);
	}
	public bool getShield(){
		return this.shield;
	}
	public int getRegenHP(){
		return this.regenHP;
	}
	public int getRegenMP(){
		return this.regenMP;
	}
	public void setRegenMP(int a){
		this.regenMP = a;
	}
	public void setRegenHP(int a){
		this.regenHP = a;
	}
	public void increaseMana(int n){
		int dif = this.getMP()+n;
		if (dif > this.getMAXMP()){
			this.setMP (this.getMAXMP());
		}else{
			this.setMP(dif);
		}
	}
	public void increaseHeal(int n){
		int dif = this.getHP()+n;
		if (dif > this.getMAXHP()){
			this.setHP (this.getMAXHP());
		}else{
			this.setHP(dif);
		}
	}
	public bool substractManaSpell(int n){
		int dif = this.getMP() - n;
		if (dif >= 0) {
			//Debug.Log ("Substracting mana: " + n);
			this.setMP(dif);
			return true;
		}
		return false;
	}
	
	// Use this for initialization
	void Start () {
		this.freeze = 0.0f;
		PJAudio = GameObject.FindObjectOfType(typeof(PJMusicManager)) as PJMusicManager;
		this.paused = false;
		
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walk", false);
		anim.SetBool ("attackMelee",false);
		
		controller = this.GetComponent<CharacterController>();
		
		PJAudio = GameObject.FindObjectOfType(typeof(PJMusicManager)) as PJMusicManager;
		
		targetPosition = transform.position;
		lastPosition = transform.position;
		hit = new RaycastHit();
		nextMagicAttack = 0;


		Magia = GameObject.FindObjectOfType(typeof(EmitParticles)) as EmitParticles;
		ParticulesSang = GameObject.FindObjectOfType(typeof(BitParticles)) as BitParticles;

		this.modeEasy ();

	}
	public override void onAttackReceived(int dmg){
		//Debug.Log("MainPjMovement: onAttackReceived");
		Debug.Log ("pj dmg: " + dmg);
		this.setHP (this.getHP () - dmg+this.getFOR());


		ParticulesSang.SpiderBit (transform.position);

		//si s'ha mort, cridar escena de morir
		if (this.getHP () <= 0) {
			//Application.LoadLevel();
			PJAudio.PlayKilled ();
			anim.SetBool ("Die", true);				
		} else {
			PJAudio.PlayHurt();
		}

	}
	//Update is called once per frame
	void FixedUpdate  () {
		
		
		if (! this.paused) {
			if (freeze <= 0.0) {
				//Magia de Foc apretant la tecla 1
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					Debug.Log ("Apretat 1");
					nextMagicAttack = 1;
				}
				
				
				if (Input.GetMouseButton(0)){
					this.clickPressed = true;
				}else{
					if (Input.GetMouseButtonUp(0)){
						this.clickPressed = false;
					}
				}
				if (this.leftPressedMouse()){
					anim.SetBool ("Walk", true);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, ~(1 << 8))) {
						if (hit.transform) {
							
							targetPosition = hit.point;
							targetPosition.y = 0;
							
							anim.SetBool ("attackMelee",false);
							
							if(this.isAlive())
								PJAudio.PlayWalkSounds();
						}
						
					}
				}
				
				
				
				//RaycastHit hit; // cast a ray from mouse pointer: 
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // if enemy hit... 
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, ~(1 << 8)) && hit.transform.CompareTag ("Enemy") && Input.GetMouseButtonDown (1)) { 
					//distancia entre el personatge principal i l'enemic
					//es calcula en metres


					
					//obtinc la aranya
					AbstractEntity Aranya = (AbstractEntity) hit.collider.GetComponent ("AbstractEntity");
					float z = transform.rotation.z;
					transform.LookAt(hit.point);
					Quaternion r = transform.rotation;
					r.z = z;
					transform.rotation = r;

					float distancia = (transform.position - Aranya.transform.position).magnitude - Aranya.distanceRadiusReduction;
					Debug.Log ("Distancia:" + distancia);

					
					switch (nextMagicAttack) {
					case 1:
						
						if (distancia > 90) {
							Debug.Log ("Cal fer una magia de foc pero estas massa lluny");
						} else {
							//anim.setBool("spellFire",true);
							if (this.substractManaSpell ((int)(manaSpellCost*this.getMAXMP()))) {
								//so de llencar la magia de foc
								//animacio de la magia
								Magia.throwParticle(this.gameObject, hit.point); //Merge devel
								anim.SetBool("magic",true);
								Aranya.onAttackReceived (Random.Range (150, 200));
							} else {
								//so de que no te magia suficient?
							}
							Debug.Log ("Magia de foc!");
							
							nextMagicAttack = 0;
							
						}
						
						break;
					case 0:
						if (distancia > physicalAttackRange) {
							//cal restar vida de l'aranya, parlar amb Jordi
							Debug.Log("Distancia: " + distancia);
							Debug.Log ("No puc atacar cos a cos");
						} else {
							//ataco a l'aranya
							Aranya.onAttackReceived (this.getDMG ());
							int probFailAttack = Random.Range (0, 10);
							//si falla (10% dels cops fallara)
							if (probFailAttack < 1) {
								PJAudio.PlayAttackFAIL ();
							} else {
								PJAudio.PlayAttackOK();	
								Aranya.onAttackReceived (Random.Range (this.getDMG () / 2, this.getDMG ()));
							}
							anim.SetBool ("attackMelee", true);
							Debug.Log ("Atac cos a cos");
						}
						break;
					}
					
				} 
				MoveTowardsTarget (targetPosition);
				
			}
		} else {
			this.freeze -= Time.deltaTime;
		}
		//Debug.Log ("Actual pos:"+transform.position + "target pos:"+targetPosition);
	}
	
	
	public void setFreeze(float n){
		this.freeze = n;
	}
	public float getFreeze(){
		return this.freeze;
	}
	void MoveTowardsTarget(Vector3 target) {
		var offset = target - transform.position;
		
		if (offset.magnitude > 0.5f) {
			offset = offset.normalized * speed;
			controller.Move (offset * Time.deltaTime);
			Quaternion newRotation = Quaternion.LookRotation (targetPosition - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, (speed / 5) * Time.deltaTime);
			
		} else {
			anim.SetBool ("Walk", false);
			PJAudio.StopWalkSounds();
		}
		
		
	}
	void OnControllerColliderHit(ControllerColliderHit hit){
		if ((hit.gameObject.tag == "Untagged" || hit.gameObject.tag == "Enemy") && !this.leftPressedMouse() ) {
			anim.SetBool("Walk", false);
			targetPosition = transform.position;
		}
	}
	
}
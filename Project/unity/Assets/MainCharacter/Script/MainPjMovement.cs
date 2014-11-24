	using UnityEngine;
	using System.Collections;
	

	public class MainPjMovement :  AbstractEntity {
	private GameObject player;
	private RaycastHit hit;
	public float speed = 50;

	public int nextMagicAttack;
	public Vector3 previousPosition;
	public Vector3 targetPosition;
	private CharacterController controller;
	public float lerpMoving;
	public int i;

	private PJMusicManager PJAudio;
	public bool shield;
	private Animator anim;
	public bool paused;
	public float freeze;

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
		this.setHP (1500);
		this.setMAXHP (1500);
		this.setFOR (0);
		controller = this.GetComponent<CharacterController>();

		PJAudio = GameObject.FindObjectOfType(typeof(PJMusicManager)) as PJMusicManager;

		targetPosition = transform.position;
		hit = new RaycastHit();
		nextMagicAttack = 0;
	}
	public override void onAttackReceived(int dmg){

		this.setHP (this.getHP () - dmg+this.getFOR());

		//si s'ha mort, cridar escena de morir
		if (this.getHP () <= 0) {
			//Application.LoadLevel();
			PJAudio.PlayKilled();
			anim.SetBool ("Die", true);				
		}
	}
	//Update is called once per frame
	void Update () {


			if (! this.paused) {
						if (freeze <= 0.0) {
								//Magia de Foc apretant la tecla 1
								if (Input.GetKeyDown (KeyCode.Alpha1)) {
										Debug.Log ("Apretat 1");
										nextMagicAttack = 1;

								}

								if (Input.GetMouseButtonDown (0)) {
										if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000.0f, ~(1 << 8))) {
												if (hit.transform) {
														targetPosition = hit.point;
														targetPosition.y = 0;
														anim.SetBool ("Walk", true);
														if(this.isAlive())
															PJAudio.PlayWalkSounds();
												}

										}
								}



								//RaycastHit hit; // cast a ray from mouse pointer: 
								Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // if enemy hit... 
								if (Physics.Raycast (ray, out hit) && hit.transform.CompareTag ("Spider") && Input.GetMouseButtonDown (1)) { 
										//distancia entre el personatge principal i l'enemic
										//es calcula en metres
										float distancia = hit.distance;
										Debug.Log ("Distancia:" + distancia);

										//obtinc la aranya
										SpiderState Aranya = (SpiderState)hit.collider.GetComponent ("SpiderState");

										switch (nextMagicAttack) {
										case 1:

												if (distancia > 90) {
														Debug.Log ("Cal fer una magia de foc pero estas massa lluny");
												} else {
														//anim.setBool("spellFire",true);
														if (this.substractManaSpell (this.getMAXMP())) {
																//so de llencar la magia de foc
																//animacio de la magia
																Aranya.onAttackReceived (Random.Range (100, 200));
														} else {
																//
														}
														Debug.Log ("Magia de foc!");
														nextMagicAttack = 0;

												}

												break;
										case 0:
												if (distancia > 80) {
														//cal restar vida de l'aranya, parlar amb Jordi
														Debug.Log ("No puc atacar cos a cos");
												} else {
														//ataco a l'aranya
														Aranya.onAttackReceived (this.getFOR ());
														int probFailAttack = Random.Range (0, 10);
														//si falla (10% dels cops fallara)
														if (probFailAttack < 1) {
																PJAudio.PlayAttackFAIL ();
														} else {
																PJAudio.PlayAttackOK();	
																Aranya.onAttackReceived (Random.Range (this.getFOR () / 2, this.getFOR ()));
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

}
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

	private Animator anim;




	public int getSelectedSpell(){
		return nextMagicAttack-1;
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walk", false);
		this.setHP (1500);
		this.setMAXHP (1500);
		this.setFOR (30);
		controller = this.GetComponent<CharacterController>();

		PJAudio = GameObject.FindObjectOfType(typeof(PJMusicManager)) as PJMusicManager;

		targetPosition = transform.position;
		hit = new RaycastHit();
		i = 0;
		nextMagicAttack = 0;
	}
	public override void onAttackReceived(int dmg){
		this.setHP (this.getHP () - dmg);
		//si s'ha mort, cridar escena de morir
		if (this.getHP () <= 0) {
			//Application.LoadLevel();
			PJAudio.PlayKilled();
			anim.SetBool ("Die", true);				
		}
	}
	//Update is called once per frame
	void Update () {
		//prova per morir
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			anim.SetTrigger("attackMelee");
			/*anim.SetBool("attackMelee",false);
			anim.SetBool("attackMelee",true);*/
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			anim.SetBool("Die",true);
		}
		//Magia de Foc apretant la tecla 1
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log("Apretat 1");
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
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // if enemy hit... 
		if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Spider") && Input.GetMouseButtonDown (1)){ 
				//distancia entre el personatge principal i l'enemic
				//es calcula en metres
				float distancia = hit.distance;
				Debug.Log("Distancia:"+distancia);

				//obtinc la aranya
				AbstractEntity Aranya = (AbstractEntity)hit.collider.GetComponent("Spider");
					
				switch(nextMagicAttack){
				case 1:

					if (distancia > 90){
						Debug.Log("Cal fer una magia de foc pero estas massa lluny");
					}else{
						//cal restar mana
						//cal restar vida de l'aranya, parlar amb Jordi
						//cal cridar animacio magia de foc

						//Aranya.onAttackReceived(80);
						//anim.setBool("spellFire",true);
						Debug.Log("Magia de foc!");
						nextMagicAttack = 0;

					}

					break;
				case 2:
					//magia de gel
					if (distancia > 90){
						Debug.Log("Cal fer una magia de foc pero estas massa lluny");
					}else{
						//cal restar mana
						//cal restar vida de l'aranya, parlar amb Jordi
						//cal cridar animacio magia de gel
						Debug.Log("Magia de foc!");
						nextMagicAttack = 0;
					}
					break;
				case 0:
					if (distancia > 80){
						//cal restar vida de l'aranya, parlar amb Jordi
						Debug.Log("No puc atacar cos a cos");
					}else{
						//ataco a l'aranya
						//Aranya.onAttackReceived(this.getFOR());
						PJAudio.PlayAttackOK();	
						anim.SetBool("attackMelee",true);
						Debug.Log("Atac cos a cos");
					}
					break;
				}

			} 
			
			MoveTowardsTarget (targetPosition);

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
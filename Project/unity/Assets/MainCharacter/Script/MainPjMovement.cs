	using UnityEngine;
	using System.Collections;
	

	public class MainPjMovement : MonoBehaviour {
	private GameObject player;
	private RaycastHit hit;
	public float speed = 50;

	public int nextMagicAttack;
	public Vector3 previousPosition;
	public Vector3 targetPosition;
	private CharacterController controller;
	public float lerpMoving;
	public int i;
	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		targetPosition = transform.position;
		hit = new RaycastHit();
		i = 0;
		nextMagicAttack = 0;
	}
	
	//Update is called once per frame
	void Update () {
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
				}
			}



			//RaycastHit hit; // cast a ray from mouse pointer: 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // if enemy hit... 
			if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Spider")){ 
				//distancia entre el personatge principal i l'enemic
				//es calcula en metres
				float distancia = hit.distance;
				Debug.Log("Distancia:"+distancia);

				//obtinc la aranya
				NPCState Aranya = (NPCState)hit.collider.GetComponent("Spider");
					
				switch(nextMagicAttack){
				case 1:
					//cridar animacio magia de foc
					//aranya.receiveDamage(80);
					if (distancia > 90){
						Debug.Log("Cal fer una magia de foc pero estas massa lluny");
						nextMagicAttack = 0;
					}else{
						Debug.Log("Magia de foc!");
					}

					break;
				case 0:
					if (distancia > 80){
						Debug.Log("No puc atacar cos a cos");
					}else{
						Debug.Log("Atac cos a cos");
					}
					break;
				}

			} 
			Debug.Log("Ara nextMagicAttack val"+ nextMagicAttack);
		}

		MoveTowardsTarget (targetPosition);
	}

	void MoveTowardsTarget(Vector3 target) {
		var offset = target - transform.position;
		if(offset.magnitude > 0.5f) {
			offset = offset.normalized * speed;
			controller.Move(offset * Time.deltaTime);
			Quaternion newRotation = Quaternion.LookRotation (targetPosition - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation,  (speed/5)*Time.deltaTime);
		}
	}

}
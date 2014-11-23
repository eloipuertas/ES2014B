using UnityEngine;
using System.Collections;

public class EnvController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Sesta carregant EnvController al papertext");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	 * Cal verificar el out!! no funciona be...
	 * 
	 */
	void increaseHeal( MainPjMovement target, int num){
		target.setHP(target.getHP()+num);
		if (target.getHP() > target.getMAXHP()){
			target.setHP (target.getMAXHP());
		}
	}
	/*
	 * Cal verificar el out!! no funciona be...
	 * 
	 */
	void increaseMana(MainPjMovement target, int num){
		target.setHP(target.getMP()+num);
		if (target.getMP() > target.getMAXMP()){
			target.setMP (target.getMAXMP());
		}
	}	


	void OnControllerColliderHit(ControllerColliderHit hit){
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		MainPjMovement target = go.GetComponent ("MainPjMovement") as MainPjMovement;
		if (hit.gameObject.tag == "BigHealPotion") {
			target.increaseHeal(200);
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "LittleHealPotion") {
			target.increaseHeal(100);
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "BigManaPotion") {
			target.increaseMana(200);
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "BigManaPotion") {
			target.increaseMana(100);
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "Shield") {
			target.setShield(true);
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "Rocket") {
			target.setFreeze(2.0f);
			Destroy(hit.gameObject);
		}
	}
}

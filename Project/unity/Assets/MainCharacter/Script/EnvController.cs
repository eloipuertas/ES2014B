using UnityEngine;
using System.Collections;

public class EnvController : MonoBehaviour {


	private PJMusicManager PJAudio;


	// Use this for initialization
	void Start () {
		//Debug.Log ("Sesta carregant EnvController al papertext");
		PJAudio = GameObject.FindObjectOfType(typeof(PJMusicManager)) as PJMusicManager;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//augmenta la vida sense passar-se del limit de vida
	void increaseHeal( MainPjMovement target, int num){
		target.setHP(target.getHP()+num);
		if (target.getHP() > target.getMAXHP()){
			target.setHP (target.getMAXHP());
		}
	}
	//augmenta el mana sense passar-se del limit de mana
	void increaseMana(MainPjMovement target, int num){
		target.setHP(target.getMP()+num);
		if (target.getMP() > target.getMAXMP()){
			target.setMP (target.getMAXMP());
		}
	}	
	
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		MainPjMovement target = go.GetComponent ("MainPjMovement") as MainPjMovement;

		if (target.getHP () != target.getMAXHP ()) {
			if (hit.gameObject.tag == "BigHealPotion") {
				PJAudio.DrinkPotion();
				target.increaseHeal (200);
				Destroy (hit.gameObject);
			}
			if (hit.gameObject.tag == "LittleHealPotion") {
				PJAudio.DrinkPotion();
				target.increaseHeal (100);
				Destroy (hit.gameObject);
			}
		}
		if (target.getMP () != target.getMAXMP ()) {
			if (hit.gameObject.tag == "BigManaPotion") {
				target.increaseMana (200);
				Destroy (hit.gameObject);
			}
			if (hit.gameObject.tag == "BigManaPotion") {
				target.increaseMana (100);
				Destroy (hit.gameObject);
			}
		}
		if (hit.gameObject.tag == "Shield") {
			target.setShield(true);
			
			Destroy(hit.gameObject);
		}
	}
}

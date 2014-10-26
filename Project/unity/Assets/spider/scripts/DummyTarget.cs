using UnityEngine;
using System.Collections;

public class DummyTarget : MonoBehaviour, IAttacker {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int attack(IAttacker attacker){
		return NPCState.VIVO;
	}
	
	public int receiveDamage(int damage){
		return NPCState.VIVO;
	}
	
	public int getState(){
		return NPCState.VIVO;
	}
}

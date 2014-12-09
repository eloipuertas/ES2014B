using UnityEngine;
using System.Collections;

public class RegenerationPj : MonoBehaviour {
	public float time;
	public MainPjMovement pj;
	// Use this for initialization
	void Start () {
		this.time = 1.0f;
		pj = this.GetComponent ("MainPjMovement") as MainPjMovement;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.time > 0){
			this.time -= Time.deltaTime;
		}else{
			this.time = 1.0f;
			if (pj.isAlive()){
				pj.increaseHeal(pj.getRegenHP());
				pj.increaseMana(pj.getRegenMP());
			}
		}
	}
}

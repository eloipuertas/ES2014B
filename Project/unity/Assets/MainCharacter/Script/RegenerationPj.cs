using UnityEngine;
using System.Collections;

public class RegenerationPj : MonoBehaviour {
	public float time;
	public int regenHP = 2;
	public int regenMP = 5;
	public MainPjMovement pj;
	// Use this for initialization
	void Start () {
		this.time = 1.0;
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		MainPjMovement pj = go.GetComponent ("MainPjMovement") as MainPjMovement;
	}
	
	// Update is called once per frame
	void Update () {

		if (this.time > 0){
			this.time -= Time.deltaTime;
		}else{
			this.time = 1.0;
			pj.increaseHeal(this.regenHP);
			pj.increaseMana(this.regenMP);
		}
	}
}

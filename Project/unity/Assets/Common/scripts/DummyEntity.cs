using UnityEngine;
using System.Collections;

public class DummyEntity : AbstractEntity {

	// Use this for initialization
	void Start () {
		HP = 100;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void onAttackReceived (int baseDMG)
	{
		//HP = HP - baseDMG;
	}

}

using UnityEngine;
using System.Collections;

public class SpiderTrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void destroyIn(float seconds){
		Invoke("destroyObject",seconds);
	}

	void destroyObject(){
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

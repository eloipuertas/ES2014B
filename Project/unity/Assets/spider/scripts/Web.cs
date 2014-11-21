using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		this.gameObject.SetActive (false);
		Invoke ("destroyObject",5);
	}

	void destroyObject(){
		Destroy(gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

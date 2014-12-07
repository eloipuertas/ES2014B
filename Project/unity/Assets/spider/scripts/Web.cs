using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {
	void Awake () {
		Invoke ("destroyObject",5f);
	}
	/**
	void OnCollisionEnter(Collision collision) {
		Invoke ("destroyObject",5f);
		this.gameObject.SetActive (false);
		Debug.Log (transform.position);
		Debug.Log (collision.collider.tag);
	}
	**/
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

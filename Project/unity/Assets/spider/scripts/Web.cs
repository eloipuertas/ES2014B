using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag!="Player"&&collision.collider.tag!="Rocket"){
			this.gameObject.SetActive (false);
			Invoke ("destroyObject",5f);
			Debug.Log (transform.position);
			Debug.Log (collision.collider.tag);
		}
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

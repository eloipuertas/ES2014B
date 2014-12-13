using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {
	private Transform target;
	void Awake () {
		Invoke ("destroyObject",5f);
	}

	public void setTarget(Transform tar){
		target = tar;
	}
	void OnCollisionEnter(Collision collision) {
		this.gameObject.SetActive (false);
		MainPjMovement pjcontrol = target.GetComponent<MainPjMovement> ();
		//Debug.Log ((transform.position-target.position).magnitude);
		if (pjcontrol!=null && (transform.position-target.position).magnitude<20){
			pjcontrol.setFreeze (2.0f);
		}
		//Debug.Log (transform.position);
		//Debug.Log (collision.collider.tag);
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

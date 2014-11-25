using UnityEngine;
using System.Collections;

public class triggerDoor : MonoBehaviour {
	public Transform door;      
	private Animator anim;
	private BoxCollider collider;

	void Awake ()
	{
		anim = door.GetComponent<Animator>();
		collider = door.GetComponent<BoxCollider>();

		renderer.enabled = false;
		collider.enabled = false;

		anim.SetBool ("open", false);
		anim.SetBool ("closed", false);
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("OnTriggerEnter");

		if ( !anim.GetBool("open") && anim.GetBool("closed") ) {
			anim.SetBool("closed", false);
		}

		anim.SetBool ("open", true);
	}
	
	
	void OnTriggerExit (Collider other)
	{
		Debug.Log ("OnTriggerExit");
		anim.SetBool ("open", false);
		anim.SetBool ("closed", true);
		collider.enabled = true;
	}
	
	
	void Update ()
	{

	}
}

using UnityEngine;
using System.Collections;

public class triggerDoor : MonoBehaviour {
	public Transform door;      
	private Animator anim;
	
	void start ()
	{

	}
	void Awake ()
	{
		anim = door.GetComponent<Animator>();

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
	}
	
	
	void Update ()
	{

	}
}

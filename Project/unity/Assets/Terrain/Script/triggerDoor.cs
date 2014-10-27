using UnityEngine;
using System.Collections;

public class triggerDoor : MonoBehaviour {
	public Transform door;                  
	//private GameObject player;
	private Animator anim;
	
	void start ()
	{

	}
	void Awake ()
	{
		anim = door.GetComponent<Animator>();
		anim.SetBool ("player_approaching", false);
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		anim.SetBool ("player_approaching", true);
	}
	
	
	void OnTriggerExit (Collider other)
	{
		anim.SetBool ("player_approaching", false);
	}
	
	
	void Update ()
	{

	}
}

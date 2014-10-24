	using UnityEngine;
	using System.Collections;
	

	public class MainPjMovement : MonoBehaviour {
	private GameObject player;
	private RaycastHit hit;
	public float speed = 50;
	
	public Vector3 previousPosition;
	public Vector3 targetPosition;
	private CharacterController controller;
	public float lerpMoving;
	
	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		targetPosition = transform.position;
		hit = new RaycastHit();
	}
	
	//Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000.0f, ~(1 << 8))) {
				if (hit.transform) {
					targetPosition = hit.point;
					targetPosition.y = 0;
				}
			}
		}

		MoveTowardsTarget (targetPosition);
	}

	void MoveTowardsTarget(Vector3 target) {
		var offset = target - transform.position;
		if(offset.magnitude > 0.5f) {
			offset = offset.normalized * speed;
			controller.Move(offset * Time.deltaTime);
			Quaternion newRotation = Quaternion.LookRotation (targetPosition - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation,  (speed/5)*Time.deltaTime);
		}
	}

}
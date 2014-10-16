	using UnityEngine;
	using System.Collections;
	

	public class MainPjMovement : MonoBehaviour {
	private GameObject player;
	private RaycastHit hit;
	public Vector3 playerPos = new Vector3 (75,0,75);
	public float speed = 50.0f;
	
	public Vector3 previousPosition;
	public Vector3 targetPosition;
	public float lerpMoving;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("mainCharacter");
		targetPosition = player.transform.position;
		hit = new RaycastHit();
	}
	
	//Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 1000.0f)) {
					previousPosition = transform.position;
					targetPosition = hit.point;
					//targetPosition.y = GameObject.Find("Terrain").transform.position.y;
					targetPosition.y = 0;
					lerpMoving = 0;
				}
			}
		}
		//if(lerpMoving < 1)
			movePlayer();
	}
	
	void movePlayer(){
		//lerpMoving += Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
	}
}
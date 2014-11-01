using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public float smooth = 1.5f;         // suavitat de moviment de la camera
	public Vector3 posCamera;
	
	private Transform player;           
	private Vector3 relCameraPos;       
	private float relCameraPosMag;      
	private Vector3 newPos;             
	private float distanciaMin = 0f;
	private float distanciaMax = 10.0f;
	private float zoomSpeed = 1.0f;
	private float distancia;

	
	void Awake ()
	{

		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		posCamera = new Vector3 (-0.25f, 0.3f, -0.25f);
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude; //- 0.5f;

	}
	
	
	void FixedUpdate ()
	{

		Vector3 standardPos = player.position + relCameraPos;
		distancia = Mathf.Clamp (Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);


		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		


		Vector3[] checkPoints = new Vector3[5];
		

		checkPoints[0] = standardPos;
		

		checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
		

		checkPoints[4] = abovePos;
		

		for(int i = 0; i < checkPoints.Length; i++)
		{
			//si la camera pot veure al player parem
			if(ViewingPosCheck(checkPoints[i]))

				break;
		}
		//Debug.Log (distancia);

		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime)+posCamera;
	
		//distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);

		SmoothLookAt();
	}
	
	
	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;
		

		if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
		
			if(hit.transform != player)
		
				return false;
		
	
		newPos = checkPos;
		return true;
	}
	
	
	void SmoothLookAt ()
	{
	
		Vector3 relPlayerPosition = player.position - transform.position;
		
	
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		Debug.Log (smooth * Time.deltaTime);
	
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
}
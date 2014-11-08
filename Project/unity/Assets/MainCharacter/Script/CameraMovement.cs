using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
		public float smooth = 1.5f;         // suavitat de moviment de la camera
		private Vector3 posCamera = new Vector3 (0f, 0f, 0f);
		public Vector3 distCamera = new Vector3 (-20f, 15f, -20f);
		private Transform player;
		private Vector3 relCameraPos;
		private float relCameraPosMag;
		private Vector3 newPos;
		public int distanciaMin = 0;
		public int distanciaMax = 10;
		private float zoomSpeed = 4.0f;
		private float distancia, distanciaAnt = 0;
		private Vector3 antigaPos = new Vector3 (0, 0, 0);
	
		void Awake ()
		{

				player = GameObject.FindGameObjectWithTag ("Player").transform;
		
				//posCamera = new Vector3 (-0.25f, 0.3f, -0.25f);
				
				relCameraPos = transform.position - player.position;
				relCameraPosMag = relCameraPos.magnitude; //- 0.5f;

		}
	
		void FixedUpdate ()
		{

				Vector3 standardPos = player.position + relCameraPos;
				
				Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		


				Vector3[] checkPoints = new Vector3[5];
		

				checkPoints [0] = standardPos;
		

				checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
				checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.5f);
				checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
		

				checkPoints [4] = abovePos;

				

				for (int i = 0; i < checkPoints.Length; i++) {
						//si la camera pot veure al player parem
						if (ViewingPosCheck (checkPoints [i])) {
								
								break;
						}
				}
				//Debug.Log (distancia);

				float actWheel = Input.GetAxis ("Mouse ScrollWheel");
				
				distancia = Mathf.Clamp (actWheel * zoomSpeed, distanciaMin, distanciaMax);
				
				//Debug.Log (distancia);

				
				/*if (distanciaAnt != distancia && actWheel != 0) {
						for (int i = 0; i < 3; i++) {
								if (i == 1) {

										posCamera [i] = distancia;
										//posCamera [i] = Mathf.Min (posCamera [i], 0.3f);
								} else
										posCamera [i] = distancia;
								
						}
						
						distanciaAnt = distancia;
				}*/
				

				//for (int i = 0; i < 3; i++) 						Debug.Log ("posCamera[" + i + "]= " + posCamera [i]);
				/*if (potVeure) {
						transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime) + posCamera + distCamera;
				} else {
						transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime)-distCamera;
				}*/

				//si no el veig a +dist
				//posar camera a transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime)
				//si el veig a +dist
				//posar camera a +dist
				
				if (ViewingPosCheck (Vector3.Lerp (transform.position, abovePos + distCamera, smooth * Time.deltaTime))) {
						
						
						//transform.position = abovePos + distCamera;
						transform.position = Vector3.Lerp (transform.position, abovePos+distCamera, smooth * Time.deltaTime);
						
						
					
				} else {
						transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime);
				}
				SmoothLookAt ();
		}
	
		bool ViewingPosCheck (Vector3 checkPos)
		{
				RaycastHit hit;
		

				if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag))
		
				if (hit.transform != player)
		
						return false;
		
	
				newPos = checkPos;
				return true;
		}
	
		void SmoothLookAt ()
		{
	
				Vector3 relPlayerPosition = player.position - transform.position;
		
	
				Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
				
	
				transform.rotation = Quaternion.Lerp (transform.rotation, lookAtRotation, smooth * Time.deltaTime);
		}
}
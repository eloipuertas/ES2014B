using UnityEngine;
using System.Collections; 

using System.Collections.Generic;

using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	
		public Vector3 posCamera = new Vector3 (9, 25, 9);
		public float distanciaMin = 30f;
		public float distanciaMax = 50f;
		public int marge = 20;
		public GameObject playerGo;
		public Transform player;
		public Vector3 relCameraPos;
		public float relCameraPosMag;
		public Vector3 newPos;
		public float zoomSpeed = 4.0f;
		public float distancia, distanciaAnt = 0;
		public float smooth = 1.5f;         // suavitat de moviment de la camera
		private List<GameObject> amagats;
		private Vector3 firstMovement;
		
		void Awake ()
		{
				updatePlayerGo ();
		}

		void updatePlayerGo ()
		{
				if (player == null) {
						playerGo = GameObject.FindGameObjectWithTag ("Player");
						if (playerGo != null) {
								player = playerGo.transform;
								amagats = new List<GameObject> ();
				
								firstMovement = player.transform.position;
								transform.position = posCamera;
								transform.LookAt (player.position);
				
				
								relCameraPos = transform.position - player.position;
								relCameraPosMag = relCameraPos.magnitude; //- 0.5f;
						}
				}
		}
	
		void Update ()
		{
				updatePlayerGo ();
				if (playerGo == null) {
						return;
				}

				if (Mathf.Abs (player.transform.position.x - firstMovement [0]) > 2 && Mathf.Abs (player.transform.position.z - firstMovement [2]) > 3) {
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
								ViewingPosCheck (checkPoints [i]);
								/*if (ViewingPosCheck (checkPoints [i]))
				
										break;*/
						}
						ViewingPosCheck (transform.position);
						bool enmig;
						for (int j = 0; j < amagats.Count; j++) {
						
								enmig = changeAlpha (amagats [j]);
								if (enmig)
										amagats.Remove (amagats [j]);
								
						
						}
				
		
						distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);


						posCamera [0] = player.position.x - distancia;
						posCamera [1] = player.position.y + distancia;
						posCamera [2] = player.position.z - distancia;			

						transform.position = posCamera;

						transform.LookAt (player.position);
				}
		}

		void ViewingPosCheck (Vector3 checkPos)
		{
				RaycastHit hit;
				Vector4 color = new Vector4 (0, 0, 0, 0);
			
				if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag)) {
						Debug.Log("objecte que xoca = "+hit.transform.gameObject.name +" Te renderer?= "+hit.transform.gameObject.renderer);
						if (hit.transform != player) {
								if (hit.transform.gameObject.renderer != null) {
										//if (hit.transform.gameObject.layer == 8) {
										//hit.transform.gameObject.renderer.enabled = false;
						
										color = hit.transform.gameObject.renderer.material.color;
						
										hit.transform.gameObject.renderer.material.shader = Shader.Find ("Transparent/Diffuse");
						
										color [3] = 0.5f;
						
										hit.transform.gameObject.renderer.material.color = color;
						
										amagats.Add (hit.transform.gameObject);
								}
					
						}
				
				}
			
		}

		bool changeAlpha (GameObject obj)
		{
				if (obj != null) {
						Vector3 posObj = obj.transform.position;


						if ((transform.position.x - posObj [0]) > marge || (posObj [0] - player.transform.position.x) > marge && (transform.position.z - posObj [2]) > marge || (posObj [2] - player.transform.position.z) > marge) {
								//no transparent
						
								obj.renderer.material.shader = Shader.Find ("Diffuse");
						
								return true;

						} else {//transparent
						
								return false;
						}
				} else {
						return true;
				}
		}

}


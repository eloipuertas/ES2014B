using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

		public GameObject target = null;
		private Vector3 posCamera = new Vector3 (0, 0, 0);
		private float smooth = 5;
		public float distancia = 5f;
		public float distanciaMin = 3.0f;
		public float distanciaMax = 10.0f;
		public float zoomSpeed = 2.0f;
		public Vector3 posInit = new Vector3 (0, 0, 0);
		
		void Start ()
		{
				target = GameObject.FindGameObjectWithTag ("Player");
				//transform.position = posInit;
		}

		private void Update ()
		{
				if (target != null) {

						transform.LookAt (target.transform);

						distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);

						//posCamera [1] = distancia;
						//posCamera [2] = -distancia;

						posCamera [0] = target.transform.position.x - distancia;
						posCamera [1] = target.transform.position.y + distancia;
						posCamera [2] = target.transform.position.z - distancia;			

						//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x -20, transform.localEulerAngles.y, transform.localEulerAngles.z);
					
						
						transform.position = posCamera;
				}

		}
}

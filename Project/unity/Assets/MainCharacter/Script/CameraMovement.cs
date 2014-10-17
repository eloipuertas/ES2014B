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
				transform.position = posInit;
		}

		private void Update ()
		{

				if (target != null) {
						
						transform.LookAt (target.transform);

						posInit [0] = Mathf.Lerp (transform.position.x, target.transform.position.x, Time.deltaTime * smooth);
						posInit [1] = Mathf.Lerp (transform.position.y, target.transform.position.y, Time.deltaTime * smooth);
						posInit [2] = Mathf.Lerp (transform.position.z, target.transform.position.z, Time.deltaTime * smooth);
						
						distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);

						posCamera [0] = posInit [0] + distancia;
						posCamera [1] = posInit [1] + distancia;
						posCamera [2] = posInit [2] + distancia;
						
						transform.position = posCamera;
						
						
				}

		}
}

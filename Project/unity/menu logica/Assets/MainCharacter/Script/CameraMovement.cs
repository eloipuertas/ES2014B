using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

		public GameObject target = null;
		private Vector3 posCamera = new Vector3 (0, 0, 0);
		private float smooth = 5;
		private float xCamera = 0, yCamera = 0, zCamera = 0;
		public float distancia = 5f;
		public float distanciaMin = 3.0f;
		public float distanciaMax = 10.0f;
		public float zoomSpeed = 2.0f;
		
		void Start ()
		{
				target = GameObject.FindGameObjectWithTag ("Player");
		}

		private void Update ()
		{

				if (target != null) {
						
						transform.LookAt (target.transform);

						xCamera = Mathf.Lerp (transform.position.x, target.transform.position.x, Time.deltaTime * smooth);
						yCamera = Mathf.Lerp (transform.position.y, target.transform.position.y, Time.deltaTime * smooth);
						zCamera = Mathf.Lerp (transform.position.z, target.transform.position.z, Time.deltaTime * smooth);
						
						distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);

						posCamera [0] = xCamera + distancia;
						posCamera [1] = yCamera + distancia;
						posCamera [2] = zCamera + distancia;
						
						transform.position = posCamera;
						
						
				}

		}
}

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

		public GameObject target;
		private Vector3 posCamera = new Vector3 (0, 0, 0);
		public float distancia = 40f;
		public float distanciaMin = 30f;
		public float distanciaMax = 80.0f;
		public float zoomSpeed = 5.0f;
		public Vector3 posInit = new Vector3 (0, 0, 0);
		
		void Start ()
		{
				target = GameObject.FindGameObjectWithTag ("Player");
		}

		private void Update ()
		{
				if (target != null) {
						distancia = Mathf.Clamp (distancia + Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, distanciaMin, distanciaMax);

						posCamera [0] = target.transform.position.x - distancia;
						posCamera [1] = target.transform.position.y + distancia;
						posCamera [2] = target.transform.position.z - distancia;			

						transform.position = posCamera;
						transform.LookAt (target.transform.position);
				}

		}
}

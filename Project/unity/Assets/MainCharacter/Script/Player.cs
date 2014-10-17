using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour, InterfacePlayer
{
		private bool firstposition;
		public float posInicialX,posInicialY,posInicialZ;
		public int mass;
		void Awake ()
		{
				DontDestroyOnLoad (gameObject);

		}
		// Use this for initialization
		void Start ()
		{

				
				firstposition = false;
				
		//rigidbody.freezeRotation = true;
			

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Application.loadedLevel == 1 && !firstposition) {


						Vector3 temp = new Vector3 (posInicialX, posInicialY, posInicialZ);
						//rigidbody.mass = mass;
						this.transform.position = temp;
						firstposition = true;

				}
				


						
		}
	

		public void movePlayer (float x, float y, float z)
		{
				Debug.Log ("Not implemented yet");

		}

		
}

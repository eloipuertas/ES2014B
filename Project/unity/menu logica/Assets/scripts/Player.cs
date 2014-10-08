using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour, InterfacePlayer
{
		private bool firstposition;
		private int cont = 0;

		void Awake ()
		{
				DontDestroyOnLoad (gameObject);

		}
		// Use this for initialization
		void Start ()
		{

				this.transform.renderer.enabled = false;
				firstposition = false;
			

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Application.loadedLevel == 1 && !firstposition) {
						rigidbody.useGravity = true;
						this.transform.renderer.enabled = true;
						Vector3 temp = new Vector3 (80.83101f, 5f, 248.4657f);//temporal
						this.transform.position = temp;
						firstposition = true;
				} else if (Application.loadedLevel == 0) {
						rigidbody.useGravity = false;
						Vector3 temp = new Vector3 (0f, 5f, 0f);//temporal
						this.transform.position = temp;
				}
				


						
		}

		public void addFeature (string caracteristica)
		{
				
				PlayerPrefs.SetString ("f" + cont, caracteristica);
				cont++;
				
		}

		public void movePlayer (float x, float y, float z)
		{
				Debug.Log ("Not implemented yet");

		}

		
}

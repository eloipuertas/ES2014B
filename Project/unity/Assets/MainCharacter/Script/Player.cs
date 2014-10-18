using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
		private bool firstposition;
		public float posInicialX, posInicialY, posInicialZ;
		public int mass;
		private float timeESC;
		private GameObject mesh;

		void Awake ()
		{
				DontDestroyOnLoad (gameObject);

		}
		// Use this for initialization
		void Start ()
		{

				
				firstposition = false;
				timeESC = 0;
				mesh = GameObject.FindGameObjectWithTag ("mesh_pj");
				mesh.renderer.enabled = false;
				
				
			

		}

		private void firstESC ()
		{
				timeESC = Time.time;
				
				
		}

		private void secondESC ()
		{
				Object.Destroy (this.gameObject);
				Application.LoadLevel (0);//menu principal
				
				
		}
		// Update is called once per frame
		void Update ()
		{
				//si pulsamos escape 2 veces mientras estemos en el juego iremos al menu principal
				if (Input.GetKeyDown (KeyCode.Escape) && Application.loadedLevel > 0) { 
						float delay = (Time.time - timeESC);
						if (delay < 0.5f)
								secondESC ();
						else
								firstESC ();
				} 
				if (Application.loadedLevel == 1 && !firstposition) {

						mesh.renderer.enabled = true;
						Vector3 temp = new Vector3 (posInicialX, posInicialY, posInicialZ);
						this.transform.position = temp;
						firstposition = true;

				}
				


						
		}


		
}

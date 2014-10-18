using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
		private bool firstposition;
		public float posInicialX,posInicialY,posInicialZ;
		public int mass;
		private GameObject mesh;

		void Awake () { DontDestroyOnLoad (gameObject); }

		void Start () {
				mesh = GameObject.FindGameObjectWithTag ("mesh_pj");
				mesh.renderer.enabled = false;
		}

		void Update () {
				if (Application.loadedLevel == 1 && !firstposition) {
						mesh.renderer.enabled = true;
						Vector3 temp = new Vector3 (posInicialX, posInicialY, posInicialZ);
						this.transform.position = temp;
						firstposition = true;
				}
		}
}

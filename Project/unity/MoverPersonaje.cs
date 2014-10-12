using UnityEngine;
using System.Collections;

public class MoverPersonaje : MonoBehaviour {
	//Personaje Principal
	GameObject personajeP;
	//Coordenada de destino
	RaycastHit click; 
	
	// Use this for initialization
	void Start () {
		//TODO
		//¡cambiar a nombre del personaje!
		personajeP = GameObject.Find("Player");
		click = new RaycastHit(); 
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * A la larga, habra que ponerse de acuerdo con el desarrollador de IA
		 * para que le pase el click de destino y el me vaya devolviendo
		 * las coordenadas a las que he de mover el personaje, teniendo en cuenta
		 * los obstaculos que haya.
		 * 
		 */

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if (Physics.Raycast(ray, out click, 1000.0f)) {
					Vector3 positionDestino = new Vector3(click.point.x, 0.5f, click.point.z);
					personajeP.transform.position = positionDestino;
				}
			}
		}
	}

}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
	private bool firstposition, showPauseMenu;
	public float posInicialX, posInicialY, posInicialZ;
	public int mass;
	private float timeESC;
	private GameObject mesh;

	
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		
	}
	
	// Use this for initialization	
	void Start () {
		firstposition = false;
		timeESC = 0;
		showPauseMenu = false;	
	}
	
	private void firstESC () {
		timeESC = Time.time;
		showPauseMenu = true;
	}
	
	private void secondESC () {
		showPauseMenu = false;
		Object.Destroy (this.gameObject);
		Object.Destroy(GameObject.FindGameObjectWithTag ("Player"));
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
			Vector3 temp = new Vector3 (posInicialX, posInicialY, posInicialZ);
			this.transform.position = temp;
			firstposition = true;
			
		}
		
		
		
		
	}
	
	public bool canShowMenuPause ()
	{
		return this.showPauseMenu;
	}
	
	public void hideMenuPause ()
	{
		this.showPauseMenu = false;
	}
	
	
	
	
}
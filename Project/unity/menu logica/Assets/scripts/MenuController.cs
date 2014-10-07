using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
		private GameObject medio;
		private GameObject player;
		public bool isQuitButton = false;			//Is the button the quit button?
		private Feature mid;
	
		void Start ()
		{

				//Should the cursor be visible?
				Screen.showCursor = true;
				//The cursor will automatically be hidden, centered on view and made to never leave the view.
				//Screen.lockCursor = false;	
				medio = GameObject.FindGameObjectWithTag ("medio");
				player = GameObject.FindGameObjectWithTag ("Player");

		}
	
		//This function is called when the mouse entered the GUIElement or Collider
		public void OnMouseEnter ()
		{
				renderer.material.color = Color.red;
		
		}
		//This function is called when the mouse is not any longer over the GUIElement or Collider
		public void OnMouseExit ()
		{
				renderer.material.color = Color.yellow;
		}
	
		//This function is called when the user has released the mouse button
		public void OnMouseUpAsButton ()
		{

				if (isQuitButton) {
						Application.Quit ();
						
						
				} else {
						
						player.SendMessage("addFeature",medio.renderer.material.mainTexture.name);
						Application.LoadLevel ("PlayScene"); //Load the game (next scene)
				}
		}
}

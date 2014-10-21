using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

		public bool isQuitButton;			//Is the button the quit button?
		private Color color;
	    public Texture2D buttontex;
		public Texture2D hoverbutton;
	
		void Start ()
		{


		//scolor = renderer.material.color;

		}
	
		//This function is called when the mouse entered the GUIElement or Collider
		public void OnMouseExit ()
		{
				//renderer.material.color = Color.red;
				guiTexture.texture = buttontex;
		
		}
		public void OnMouseEnter (){
				guiTexture.texture = hoverbutton;

		}
		//This function is called when the mouse is not any longer over the GUIElement or Collider
		
	
		//This function is called when the user has released the mouse button
		public void OnMouseUpAsButton ()
		{

				if (isQuitButton) {
						Application.Quit ();
						
						
				} else {
						GameObject jug = (GameObject) Instantiate(Resources.Load("papertex22"));

						Application.LoadLevel (1); //Load the game (next scene)
						
				}
		}
}

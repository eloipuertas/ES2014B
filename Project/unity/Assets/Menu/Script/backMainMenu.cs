using UnityEngine;
using System.Collections;

public class backMainMenu : MonoBehaviour
{
		
		public Texture2D buttontex;
		public Texture2D hoverbutton;
		
		public void OnMouseExit ()
		{
				//renderer.material.color = Color.red;
				guiTexture.texture = buttontex;
		
		}

		public void OnMouseEnter ()
		{
				guiTexture.texture = hoverbutton;

		}
		//This function is called when the user has released the mouse button
		public void OnMouseUpAsButton ()
		{

				
				
				Application.LoadLevel (0); //Load the game (next scene)
				

		}
}

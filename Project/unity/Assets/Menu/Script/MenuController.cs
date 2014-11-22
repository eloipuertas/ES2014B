using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour{

		public bool isQuitButton;			//Is the button the quit button?

		public Texture2D buttontex;
		public Texture2D hoverbutton;

		public void OnMouseExit(){
				guiTexture.texture = buttontex;
		}

		public void OnMouseEnter(){
				guiTexture.texture = hoverbutton;
		}
		
		//This function is called when the user has released the mouse button
		public void OnMouseUpAsButton(){
				if(isQuitButton){
						Application.Quit();
				}else{
						Application.LoadLevel(1); //Load the game (next scene)
						
				}
		}
}
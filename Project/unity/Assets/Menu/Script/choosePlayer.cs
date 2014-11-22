using UnityEngine;
using System.Collections;

public class choosePlayer : MonoBehaviour
{

		
		public bool first;
		public bool second;
		public bool third;
		private GameObject jug = null;
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


				if (first) {
						PlayerPrefs.SetString("player", "player1");
						jug = (GameObject)Instantiate (Resources.Load ("papertex22"));
				} else if (second) {
						PlayerPrefs.SetString("player", "player2");
						jug = (GameObject)Instantiate (Resources.Load ("player2"));
				} else if (third) {
						PlayerPrefs.SetString("player", "player3");
						jug = (GameObject)Instantiate (Resources.Load ("player3"));
				}
				if (jug != null)
						Application.LoadLevel (2); //Load the game (next scene)
				
			
		}
}

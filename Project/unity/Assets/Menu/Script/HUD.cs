using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	
	
	
		private int diametro = Screen.width / 10;
	
		//atributos de la vida
		public Texture texVida;
		private float vida, alturaVida;
		private float vidapercent;
	
		//atributos del mana
		public Texture texMana;
		private float alturaMana;
		private float manapercent;
	
		//textura que saldra cuando el personaje muera
		public Texture gameOverTexture;
		private float xPos = Screen.width / 2.7f;
		private float yPos = Screen.height / 3.2f;
		//private PjPrincipal pj;descomentar al fer integracio a master
		private Player player;
		private float buttonSizeWidth, buttonSizeHeight;
		public GUISkin myskin;
		public Texture continueTexture, backMainMenuTexture, audioOFF, audioON;
		private Texture audioTexture;
		private bool sona = true, mort = false, debugInit = false;
		public bool debugON = false;
		private AmbientalMusic AmbientAudio;

		void Start ()
		{
		
		
				/*borrar comentari al fer l'integracio a master
				GameObject go = GameObject.FindGameObjectWithTag ("Player");
		
				player = go.GetComponent ("Player") as Player;
				pj = player.pj;*/
				audioTexture = audioON;
		
				
				
		
				AmbientAudio = GameObject.FindObjectOfType (typeof(AmbientalMusic)) as AmbientalMusic;
		
		
		}
	
		void OnGUI ()
		{
				xPos = Screen.width / 2.7f;
				yPos = Screen.height / 3.2f;
				//GUI.skin = myskin;
				
				buttonSizeHeight = Screen.height / 15;
				buttonSizeWidth = Screen.width / 5;
				
		/*
				if (!debugON)
						//vida = pj.getVida ();descomentar al integrar a master
						vida = 100;//borrar al integrar a master
				else if (debugON && !debugInit) {
						vida = 100;
						debugInit = true;
				}*/

				
				
				//vidapercent = vida / pj.getMaxVida ();descomentar al integrar a master
				vidapercent = 20;
				if (vidapercent < 0)
						vidapercent = 0;
				if (vidapercent > 100)
						vidapercent = 100;
		
				alturaVida = vidapercent * diametro;
		
				/*float mana = pj.getMana ();
				manapercent = mana / pj.getMaxMana ();borrar al fer integracio a master*/
				manapercent = 50;//borrar aquesta linia al fer la integracio a master
				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;
				
				alturaMana = manapercent * diametro;
		Debug.Log ("vida=" + vida + " vidapercent=" + vidapercent + " diametro=" + diametro+" manapercent=" + manapercent);

				
				GUI.BeginGroup (new Rect (Screen.width / 10, Screen.height - (alturaVida + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaVida, diametro, diametro), this.texVida);
				GUI.EndGroup ();
		
		
				
				GUI.BeginGroup (new Rect (Screen.width - Screen.width * 2 / 10, Screen.height - (alturaMana + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaMana, diametro, diametro), texMana);
				GUI.EndGroup ();
				
				/*if (player.canShowMenuPause () && !mort) {descomentar al integrar a masteer
				
						if (GUI.Button (new Rect (xPos, yPos, buttonSizeWidth, buttonSizeHeight), continueTexture)) {
								//player.hideMenuPause ();	descomentar al integrar a master

				
						}
						if (GUI.Button (new Rect (xPos, buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), audioTexture)) {
								//parar audio
							
								if (sona) {
										Debug.Log ("audio off");
										AmbientAudio.PauseAudio ();
										audioTexture = audioOFF;
								} else {
										Debug.Log ("audio on");
										AmbientAudio.UnPauseAudio ();
										audioTexture = audioON;
										
								}
								sona = !sona;
				
						}
						if (GUI.Button (new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), backMainMenuTexture)) {
								Destroy (this.gameObject);
								Application.LoadLevel (0);
							
						}
				} */
		
				//per debugar

				if (debugON && GUI.Button (new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), "Click here to lose life")) {
						
						vida -= 20;
						if (vida <= 0)
								vida = 0;


						

				}
				
				if (vida <= 0) {
						mort = true;
						AmbientAudio.PlayGameOver ();
						GUI.Label (new Rect (xPos, yPos, Screen.width / 3, Screen.height / 3), gameOverTexture);
						if (GUI.Button (new Rect (xPos * 1.1f, 2f * yPos, buttonSizeWidth, buttonSizeHeight), backMainMenuTexture)) {
								Destroy (this.gameObject);
								Application.LoadLevel (0);
				
						}
				}
		
		
		
		}
		
}

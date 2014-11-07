using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	
	
	
		private float amplada; 
		private float altura;
	
		//atributos de la vida
		public Texture texVida;
		private float vida, alturaVida;
		private float vidapercent;
	
		//atributos del mana
		public Texture texMana;
		private float alturaMana;
		private float manapercent;
		public Texture[] magicTextures = new Texture[3];
		public Texture[] magicTexturesSpelled = new Texture[3];
		
		//textura que saldra cuando el personaje muera
		public Texture gameOverTexture;
		private float xPos = Screen.width / 2.7f;
		private float yPos = Screen.height / 3.2f;
		private PjPrincipal pj;
		private Player player;
		private float buttonSizeWidth, buttonSizeHeight;
		public GUISkin myskin;
		public Texture continueTexture, backMainMenuTexture, audioOFF, audioON;
		private Texture audioTexture;
		private bool sona = true, mort = false, debugInit = false;
		public bool debugON = false;
		private AmbientalMusic AmbientAudio;
		private int  magiaEscollida = -1;

		void Start ()
		{
		
		
				
				GameObject go = GameObject.FindGameObjectWithTag ("Player");
		
				player = go.GetComponent ("Player") as Player;

				pj = player.pj;
				audioTexture = audioON;



		
				AmbientAudio = GameObject.FindObjectOfType (typeof(AmbientalMusic)) as AmbientalMusic;
		
		
		}
	
		void OnGUI ()
		{

				amplada = Screen.width / 10;
				altura = Screen.height / 8;
				xPos = Screen.width / 2.7f;
				yPos = Screen.height / 3.2f;
				GUI.skin = myskin;
				
				buttonSizeHeight = Screen.height / 15;
				buttonSizeWidth = Screen.width / 5;
				
				if (!debugON)
						vida = pj.getVida ();
				else if (debugON && !debugInit) {
						vida = 100;
						debugInit = true;
				}
				
				
				vidapercent = vida / pj.getMaxVida ();
				
				if (vidapercent < 0)
						vidapercent = 0;
				if (vidapercent > 100)
						vidapercent = 100;
		
				alturaVida = vidapercent * amplada;
		
				float mana = pj.getMana ();
				
				manapercent = mana / pj.getMaxMana ();

				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;
				
				alturaMana = manapercent * amplada;
				
				float xVida = Screen.width / 10;
				float yVida = Screen.height - (alturaVida + 10);
				float yMana = Screen.height - (alturaMana + 10);
				float xMana = Screen.width - Screen.width * 2 / 10;
				float xActual = xVida + amplada;
				float alturaMagia = Screen.height / 15;
				float yMagies = Screen.height - alturaMagia;

				if (!debugON)
						magiaEscollida = pj.getSelectedSpell ();
				/*
				GUI.BeginGroup (new Rect (xVida, yVida, altura, amplada));
				GUI.DrawTexture (new Rect (0, -amplada + alturaVida, altura, amplada), this.texVida);
				GUI.EndGroup ();
				*/
				int numTextures = magicTextures.Length + 2;
				for (int i = 0; i < numTextures; i++) {
						
						//Debug.Log ("yVida= " + yVida + " ymagies= " + (yMagies) + " heightMagia= " + (Screen.height - alturaMagia));
						if (i == 0) {//vida
								GUI.BeginGroup (new Rect (xVida, yVida, amplada, altura));
								GUI.DrawTexture (new Rect (0, -amplada + alturaVida, amplada, altura), this.texVida);
								GUI.EndGroup ();

						} else if (i == numTextures - 1) {//mana
								GUI.BeginGroup (new Rect (xMana, yMana, amplada, altura));
								GUI.DrawTexture (new Rect (0, -amplada + alturaMana, amplada, altura), this.texMana);
								GUI.EndGroup ();

						} else {//altres
								i--;

								//si hi ha 3 magies anira de 0,1,2
								Texture texturaMagia = magiaEscollida == i ? this.magicTexturesSpelled [i] : this.magicTextures [i];
				
								GUI.BeginGroup (new Rect (xActual, yMagies, alturaMagia, alturaMagia));
								GUI.DrawTexture (new Rect (0, 0, alturaMagia, alturaMagia), texturaMagia);
								GUI.EndGroup ();
								i++;
						}
						
						
						xActual += alturaMagia;
				}
			
				/*
				GUI.BeginGroup (new Rect (xMana, yVida, amplada, amplada));
				GUI.DrawTexture (new Rect (0, -amplada + alturaMana, altura, amplada), this.texMana);
				GUI.EndGroup ();
*/
				
				if (player.canShowMenuPause () && !mort) {
						
						if (GUI.Button (new Rect (xPos, yPos, buttonSizeWidth, buttonSizeHeight), continueTexture)) {
								player.hideMenuPause ();	

				
						}
						if (GUI.Button (new Rect (xPos, buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), audioTexture)) {

								if (sona) {//pausar audio
										AmbientAudio.PauseAudio ();
										audioTexture = audioOFF;
								} else {//reproduir audio
										AmbientAudio.UnPauseAudio ();
										audioTexture = audioON;
										
								}
								sona = !sona;
				
						}
						if (GUI.Button (new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), backMainMenuTexture)) {
								Destroy (this.gameObject);
								Application.LoadLevel (0);
							
						}
				} 
		
				//per debugar

				if (debugON && GUI.Button (new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), "Click here select random magic")) {
						
						/*
						vida -= 20;
						if (vida <= 0)
								vida = 0;
			
						*/
						
						magiaEscollida = Random.Range (-1, magicTextures.Length);
						Debug.Log ("magiaEscollida=  " + magiaEscollida);

				}
//				Debug.Log ("vida=" + vida);
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

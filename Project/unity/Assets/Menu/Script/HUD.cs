using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	
	
	
		private float amplada;
		private float altura;
	
		//atributos de la vida
		public Texture texVida;
		private float vida, alturaVida, vidapercent;

	
		//atributos del mana
		public Texture texMana;
		private float mana, alturaMana, manapercent;
		public Texture[] magicTextures = new Texture[3];
		public Texture[] magicTexturesSpelled = new Texture[3];
		
		//textura que saldra cuando el personaje muera
		public Texture gameOverTexture;
		private float xPos;
		private float yPos;
		//private PjPrincipal pj;descomentar a master
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
		/*
				GameObject go = GameObject.FindGameObjectWithTag ("Player");
				player = go.GetComponent ("Player") as Player;
				pj = player.pj;
		descomentar a master*/

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
				float maxVida = 100, maxMana = 100;
				if (!debugON) {
			/*
						vida = pj.getVida ();
						mana = pj.getMana ();
						magiaEscollida = pj.getSelectedSpell ();
						maxVida = pj.getMaxVida ();
						maxMana = pj.getMaxMana ();
						descomentar a master*/
					mana = 100;
					vida = 100;


		} else if (debugON && !debugInit) {
						mana = 100;
						vida = 100;
						debugInit = true;
				}
				
				vidapercent = vida / maxVida;
				

				if (vidapercent < 0)
						vidapercent = 0;
				if (vidapercent > 100)
						vidapercent = 100;
		
				alturaVida = vidapercent * altura;

				
				manapercent = mana / maxMana;

				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;
				
				alturaMana = manapercent * altura;
				
				float xVida = Screen.width / 10;
				float yVida = Screen.height - alturaVida;
				float yMana = Screen.height - alturaMana;
				float xMana = Screen.width - Screen.width * 2 / 10;
				float xActual = xVida + amplada;
				float alturaMagia = Screen.height / 15;
				float yMagies = Screen.height - alturaMagia;


				int numTextures = magicTextures.Length + 2;

				for (int i = 0; i < numTextures; i++) {
						
						
						if (i == 0) {//vida
								GUI.BeginGroup (new Rect (xVida, yVida, amplada, Screen.height - yVida));
								GUI.DrawTexture (new Rect (0, alturaVida - altura, amplada, altura), this.texVida);
								GUI.EndGroup ();

						} else if (i == numTextures - 1) {//mana
								GUI.BeginGroup (new Rect (xMana, yMana, amplada, Screen.height - yMana));
								GUI.DrawTexture (new Rect (0, alturaMana - altura, amplada, altura), this.texMana);
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

				if (debugON && GUI.Button (new Rect (0, 0, buttonSizeWidth, buttonSizeHeight), "Select random magic")) {

						magiaEscollida = Random.Range (-1, magicTextures.Length);
						Debug.Log ("magiaEscollida=  " + magiaEscollida);

				}
				if (debugON && GUI.Button (new Rect (0, buttonSizeHeight, buttonSizeWidth, buttonSizeHeight), "+ Vida")) {
			
			
						vida += 20;
						if (vida > 100)
								vida = 100;
						Debug.Log ("vida=" + vida);
			
				}
				if (debugON && GUI.Button (new Rect (0, 2 * buttonSizeHeight, buttonSizeWidth, buttonSizeHeight), "- Vida")) {
			

						vida -= 20;
						if (vida <= 0)
								vida = 0;
						

						Debug.Log ("vida=" + vida);
			
				}

				if (debugON && GUI.Button (new Rect (0, 3 * buttonSizeHeight, buttonSizeWidth, buttonSizeHeight), "+ Mana")) {
			
			
						mana += 20;
						if (mana > 100)
								mana = 100;
			
						Debug.Log ("mana=" + mana);
			
			
				}
				if (debugON && GUI.Button (new Rect (0, 4 * buttonSizeHeight, buttonSizeWidth, buttonSizeHeight), "- Mana")) {
			

						mana -= 20;
						if (mana <= 0)
								mana = 0;
			
						Debug.Log ("mana=" + mana);

			
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

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
		private PjPrincipal pj;
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
		
		
				
				GameObject go = GameObject.FindGameObjectWithTag ("Player");
		
				player = go.GetComponent ("Player") as Player;
				pj = player.pj;
				audioTexture = audioON;
		
				
				
		
				AmbientAudio = GameObject.FindObjectOfType (typeof(AmbientalMusic)) as AmbientalMusic;
		
		
		}
	
		void OnGUI ()
		{
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
		
				alturaVida = vidapercent * diametro;
		
				float mana = pj.getMana ();
				manapercent = mana / pj.getMaxMana ();
				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;
				
				alturaMana = manapercent * diametro;
				
				Debug.Log ("diametre=" + diametro + " Screen.height=" + Screen.height + " alturaVida=" + alturaVida);
				GUI.BeginGroup (new Rect (Screen.width / 10, Screen.height - (alturaVida + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaVida, diametro, diametro), this.texVida);
				GUI.EndGroup ();
		
		
				
				GUI.BeginGroup (new Rect (Screen.width - Screen.width * 2 / 10, Screen.height - (alturaMana + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaMana, diametro, diametro), texMana);
				GUI.EndGroup ();
				
				if (player.canShowMenuPause () && !mort) {
						
						if (GUI.Button (new Rect (xPos, yPos, buttonSizeWidth, buttonSizeHeight), continueTexture)) {
								player.hideMenuPause ();	

				
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
				} 
		
				//per debugar

				if (debugON && GUI.Button (new Rect (xPos, 2 * buttonSizeHeight + yPos, buttonSizeWidth, buttonSizeHeight), "Click here to lose life")) {
						
						vida -= 20;
						if (vida <= 0)
								vida = 0;


						

				}
				Debug.Log ("vida=" + vida);
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

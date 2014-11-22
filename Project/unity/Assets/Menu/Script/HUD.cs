using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	
	
	
	private float amplada;
	private float altura;
	
	//atributos de la vida
	public Texture texVida,vidaCover;
	private float vida, alturaVida, vidapercent;

	
	//atributos del mana
	public Texture texMana,manaCover;
	private float mana, alturaMana, manapercent;
	public Texture[] magicTextures = new Texture[1];
	public Texture[] magicTexturesSpelled = new Texture[1];

	public Texture texEscut;
	//textura que saldra cuando el personaje muera
	public Texture gameOverTexture,hud_bg;
	private float xPos;
	private float yPos;
	private MainPjMovement pj;
	private Player player;
	private float buttonSizeWidth, buttonSizeHeight;
	public GUISkin myskin;
	public Texture backgroundHud;
	public Texture continueTexture, backMainMenuTexture, audioOFF, audioON;
	private Texture audioTexture;
	private bool sona = true;
	private AmbientalMusic AmbientAudio;
	private int  magiaEscollida = -1;
	private float timeLeft = 2f;

	void Start ()
	{

		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		pj = go.GetComponent ("MainPjMovement") as MainPjMovement;
		player = go.GetComponent ("Player") as Player;
		
		audioTexture = audioON;
		AmbientAudio = GameObject.FindObjectOfType (typeof(AmbientalMusic)) as AmbientalMusic;
		
		
	}
	
	void OnGUI ()
	{
		Time.timeScale = 1;
		amplada = Screen.width / 10;
		altura = Screen.height / 8;
		xPos = Screen.width / 2.7f;
		yPos = Screen.height / 3.2f;
				
		GUI.skin = myskin;

		//GUI.DrawTexture (new Rect (100,478,700,120),backgroundHud);

		buttonSizeHeight = Screen.height / 15;
		buttonSizeWidth = Screen.width / 5;
		float maxVida, maxMana;
				

		vida = pj.getHP ();
		mana = pj.getMP ();
		magiaEscollida = pj.getSelectedSpell ();
		maxVida = pj.getMAXHP ();
		maxMana = pj.getMAXMP ();


				
		        
				
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
				
		float xVida = Screen.width*0.25f;
		float yVida = Screen.height - alturaVida;
		float yMana = Screen.height - alturaMana;
		float xMana = Screen.width - Screen.width * 2 / 10;
		float xActual = xVida + amplada;
		float alturaMagia = Screen.height / 15;
		float yMagies = Screen.height - alturaMagia;


		int numTextures = magicTextures.Length + 2;
		float ampladaBG = Screen.width/2;
		GUI.DrawTexture (new Rect (xVida, Screen.height-Screen.height / 8,ampladaBG,Screen.height / 8),this.hud_bg);
		for (int i = 0; i < numTextures; i++) {
						

			if (i == 0) {//vida

				GUI.BeginGroup (new Rect (xVida, yVida, amplada, Screen.height - yVida));
				GUI.DrawTexture (new Rect (0, alturaVida - altura, amplada, altura), this.texVida);
				GUI.DrawTexture (new Rect (0, alturaVida - altura, amplada, altura), this.vidaCover);
				GUI.EndGroup ();
			} else if (i == numTextures - 1) {//mana
				//GUI.BeginGroup (new Rect (xMana, yMana, amplada, Screen.height - yMana));
				GUI.BeginGroup (new Rect (xVida+ampladaBG-amplada, yMana, amplada, Screen.height - yMana));
				GUI.DrawTexture (new Rect (0, alturaMana - altura, amplada, altura), this.texMana);
				GUI.DrawTexture (new Rect (0, alturaMana - altura, amplada, altura), this.manaCover);
				GUI.EndGroup ();
			} else {//altres
				i--;

				//si hi ha 3 magies anira de 0,1,2
				Texture texturaMagia = magiaEscollida == i ? this.magicTexturesSpelled [i] : this.magicTextures [i];
				GUI.DrawTexture (new Rect (xActual, yMagies, alturaMagia, alturaMagia), texturaMagia);

				i++;
			}
						
						
			xActual += alturaMagia;
		}

		GUI.DrawTexture (new Rect (xVida+ampladaBG-amplada-alturaMagia-10, yMagies, alturaMagia, alturaMagia), texEscut);
			
				
				
		if (player.canShowMenuPause () && pj.isAlive ()) { 
			Time.timeScale = 0;
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
				Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
				Application.LoadLevel (0);
							
			}
		} 

		if (vida <= 0) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {

				AmbientAudio.PlayGameOver ();
				GUI.Label (new Rect (xPos, 0, gameOverTexture.width, gameOverTexture.height), gameOverTexture);
				Time.timeScale = 0;
				if (GUI.Button (new Rect (xPos+gameOverTexture.width*0.01f, gameOverTexture.height, gameOverTexture.width, gameOverTexture.width/2), backMainMenuTexture)) {
					Destroy (this.gameObject);
					Object.Destroy (GameObject.FindGameObjectWithTag ("Player"));
					Application.LoadLevel (0);
				
				}
			}
		}
		
		
		
	}
		
}

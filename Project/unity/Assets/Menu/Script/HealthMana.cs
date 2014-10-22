using UnityEngine;
using System.Collections;

public class HealthMana : MonoBehaviour
{
		
		
		
		private int diametro = Screen.width / 10;
		
		//atributos de la vida
		public Texture texVida;
		private float alturaVida;
		private float vida = 100;
		private float maxvida = 100;
		private float vidapercent;

		//atributos del mana
		public Texture texMana;
		private float alturaMana;
		private float mana = 100;
		private float maxmana = 100;
		private float manapercent;

		//texto que saldra cuando el personaje muera
		public Texture gameOverTexture;
		private float xPos = Screen.width / 2.7f;
		private float yPos = Screen.height / 3.2f;
		//private GUIStyle myStyle;

		
		void Start ()
		{
				/*myStyle = new GUIStyle ();
				myStyle.normal.textColor = Color.white;
				myStyle.alignment = TextAnchor.MiddleCenter;
				myStyle.normal.background = texButtonBackNotPressed;*/


		}

		void OnGUI ()
		{

				vidapercent = vida / maxvida;
				if (vidapercent < 0)
						vidapercent = 0;
				if (vidapercent > 100)
						vidapercent = 100;
				
				alturaVida = vidapercent * diametro;

				manapercent = mana / maxmana;
				if (manapercent < 0)
						manapercent = 0;
				if (manapercent > 100)
						manapercent = 100;
				
				alturaMana = manapercent * diametro;
				
				
				GUI.BeginGroup (new Rect (Screen.width / 10, Screen.height - (alturaVida + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaVida, diametro, diametro), texVida);
				GUI.EndGroup ();

				GUI.BeginGroup (new Rect (Screen.width - Screen.width * 2 / 10, Screen.height - (alturaMana + 10), diametro, diametro));
				GUI.DrawTexture (new Rect (0, -diametro + alturaMana, diametro, diametro), texMana);
				GUI.EndGroup ();

				/*
				//per debugar
				if (GUI.Button (new Rect (100, 100, 100, 100), "- vida")) {
						vida -= 20;
						if (vida <= 0) {
								
								vida = 0;


						}

				}
				*/
				if (vida <= 0) {
						
						GUI.Label (new Rect (xPos, yPos, Screen.width / 3, Screen.height / 3), gameOverTexture);
						if (GUI.Button (new Rect (xPos*1.1f, 2f * yPos, Screen.width / 5, Screen.height / 15),"Back to main menu")) {
								Destroy (this.gameObject);
								Application.LoadLevel (0);
				
						}
				}

				

		}

		public void sumarVida (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				vida += puntos;
				if (vida > maxvida)
						vida = maxvida;
		}

		public void restarVida (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				vida -= puntos;
				if (vida <= 0) {
						vida = 0;
				}
		}

		public void sumarMana (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				mana += puntos;
				if (mana > maxmana)
						mana = maxmana;
		}
	
		public void restarMana (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				mana -= puntos;
				if (mana < 0)
						mana = 0;
		}

}

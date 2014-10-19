using UnityEngine;
using System.Collections;

public class HealthMana : MonoBehaviour
{
		
		
		
		private int diametro = Screen.width / 10;
		
		//atributos de la vida
		public Texture texVida;
		private float alturaVida;
		private float hp = 100;
		private float maxhp = 100;
		private float healthpercent;

		//atributos del mana
		public Texture texMana;
		private float alturaMana;
		private float mana = 100;
		private float maxmana = 100;
		private float manapercent;

		void OnGUI ()
		{
				
				healthpercent = hp / maxhp;
				if (healthpercent < 0)
						healthpercent = 0;
				if (healthpercent > 100)
						healthpercent = 100;
				
				alturaVida = healthpercent * diametro;

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



		}

		public void sumarVida (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				hp += puntos;
				if (hp > maxhp)
						hp = maxhp;
		}

		public void restarVida (int puntos)
		{
				if (puntos < 0)
						puntos = puntos * -1;
				hp -= puntos;
				if (hp < 0) 
						hp = 0;
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

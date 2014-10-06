using UnityEngine;
using System.Collections;

public class MenuFeatures : MonoBehaviour
{

		public Texture tex1, tex2, tex3;
		public GameObject medio;
		private int clicks;

		void Start ()
		{
				
				medio = GameObject.FindGameObjectWithTag ("medio");
				medio.renderer.material.mainTexture = tex1;
				clicks = 1;

		}
	
		void OnMouseDown ()
		{
				
				

				clicks += 1;
						
				if (clicks == 1) {
						medio.renderer.material.mainTexture = tex1;
				
				} else if (clicks == 2) {
						
						//Texture2D tex = (Texture2D)Resources.Load ("imatges/destral", typeof(Texture2D));
						medio.renderer.material.mainTexture = tex2;
								
				} else if (clicks == 3) {
						medio.renderer.material.mainTexture = tex3;
						clicks = 0;
				}
						
				
			   			


		}
}

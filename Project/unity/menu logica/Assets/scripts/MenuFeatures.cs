using UnityEngine;
using System.Collections.Generic;

public class MenuFeatures : MonoBehaviour
{
	
	public Texture character_profile1, character_profile2, character_profile3;
	public Texture head1, head2, head3;
	public Texture backpack1, backpack2, backpack3;
	private GameObject medio;
	private GameObject[] left;
	private GameObject[] head;
	private int clicks;
	
	void Start ()
	{
		
		medio = GameObject.FindGameObjectWithTag ("medio");
		left = GameObject.FindGameObjectsWithTag ("left");
		head = GameObject.FindGameObjectsWithTag ("head");
		clicks = 1;
		
	}
	
	void OnMouseDown ()
	{
		
		
		
		
		
		if (clicks == 0) {
			medio.renderer.material.mainTexture = character_profile1;
			foreach( GameObject obj in left)
				obj.renderer.material.mainTexture = backpack1;
			foreach( GameObject obj in head)
				obj.renderer.material.mainTexture = head1;
			
		} else if (clicks == 1) {
			
			//Texture2D tex = (Texture2D)Resources.Load ("imatges/destral", typeof(Texture2D));
			medio.renderer.material.mainTexture = character_profile2;
			foreach( GameObject obj in left)
				obj.renderer.material.mainTexture = backpack2;
			foreach( GameObject obj in head)
				obj.renderer.material.mainTexture = head2;
			
		} else if (clicks == 2) {
			medio.renderer.material.mainTexture = character_profile3;
			foreach( GameObject obj in left)
				obj.renderer.material.mainTexture = backpack3;
			foreach( GameObject obj in head)
				obj.renderer.material.mainTexture = head3;
			clicks = -1;
		}
		clicks += 1;	
		
		
		
		
	}
}

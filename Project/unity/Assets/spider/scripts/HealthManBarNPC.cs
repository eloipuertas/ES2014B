using UnityEngine;
using System.Collections;

public class HealthBarNPC : MonoBehaviour {
	
	public float maxValueHealth;
	public float maxValueMana;
	public float progressHealth;
	public float progressMana;
	public float leftX, bottomY, width, height;
		
	public Texture2D bgImage; 
	public Texture2D fgImage;

	void Awake(){
		set (1, 1, 1, 1, 0, 0, 200, 50);
	}

	public void set(float maxHealth,float maxMana,float currentHealth,float currentMana,
	                float leftX,float bottomY,float width,float height){
		this.maxValueHealth = maxHealth;
		this.maxValueMana = maxMana;
		this.progressHealth = currentHealth;
		this.progressMana = currentMana;
		this.leftX = leftX;
		this.bottomY = bottomY;
		this.width = width;
		this.height = height;
	}
	
	// Update is called once per frame
	void Update(){
		AddjustCurrentHealth(0);
	}
	
	void OnGUI () {
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup (new Rect (this.leftX,this.bottomY, healthBarLength,32));
		
		// Draw the background image
		GUI.Box (new Rect (this.leftX,this.bottomY, healthBarLength,32), bgImage);
		
			// Create a second Group which will be clipped
			// We want to clip the image and not scale it, which is why we need the second Group
			GUI.BeginGroup (new Rect (this.leftX,this.bottomY0, curHealth / maxHealth * healthBarLength, 32));
			
			// Draw the foreground image
			GUI.Box (new Rect (this.leftX,this.bottomY,healthBarLength,32), fgImage);
			
			// End both Groups
			GUI.EndGroup ();
		
		GUI.EndGroup ();
	}
}
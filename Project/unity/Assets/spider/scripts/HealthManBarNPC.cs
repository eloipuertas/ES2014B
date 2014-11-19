using UnityEngine;
using System.Collections;

public class HealthManBarNPC : MonoBehaviour {
	
	public float maxValueHealth;
	public float maxValueMana;
	public float currentHealth;
	public float currentMana;
	public float leftX,bottomY,width,height;
		
	public Texture2D bgBar;
	public Texture2D healthBarFGTexture;
	public Texture2D manaBarFGTexture;

	void Awake(){
		set(1,1,1,1,0,0,200,50);
	}

	public void set(float maxHealth,float maxMana,float currentHealth,float currentMana,
	                float leftX,float bottomY,float width,float height){
		this.maxValueHealth = maxHealth;
		this.maxValueMana = maxMana;
		this.currentHealth = currentHealth;
		this.currentsMana = currentMana;
		this.leftX = leftX;
		this.bottomY = bottomY;
		this.width = width;
		this.height = height;
	}

	void Update(){
		// TODO ?
	}
	
	void OnGUI(){
		// BG container
		GUI.BeginGroup(new Rect(this.leftX,this.bottomY,this.width,this.height));
		GUI.Box(new Rect(this.leftX,this.bottomY,this.width,this.height),bgBar);
			
			// health FG container
			GUI.BeginGroup(new Rect(this.leftX,this.bottomY+this.height*0.5f,(this.currentHealth/this.maxValueHealth)*this.width,this.height*0.5f));
			GUI.Box(new Rect(tthis.leftX,this.bottomY+this.height*0.5f,(this.currentHealth/this.maxValueHealth)*this.width,this.height*0.5f),healthBarFGTexture);
			GUI.EndGroup();
		
			// mana FG container
			GUI.BeginGroup(new Rect(this.leftX,this.bottomY,(this.currentMana/this.maxValueMana)*this.width,this.height*0.5f));
			GUI.Box(new Rect(tthis.leftX,this.bottomY,(this.currentMana/this.maxValueMana)*this.width,this.height*0.5f),manaBarFGTexture);
			GUI.EndGroup();

		GUI.EndGroup();
	}
	
	public void setHealth(float health){
		this.currentHealth = health;
	}
	
	public void setMana(float mana){
		this.currentMana = mana;
	}
	
	public void setMaxHealth(float maxHealth){
		this.maxValueHealth = maxHealth;
	}
	
	public void setMana(float maxMana){
		this.maxValueMana = maxMana;
	}
	
	public void setLeftX(float leftX){
		this.leftX = leftX;
	}
	
	public void setBottomY(float bottomY){
		this.bottomY = bottomY;
	}
	
	public void setWidth(float width){
		this.width = width;
	}
	
	public void setHeight(float height){
		this.height = height;
	}
}
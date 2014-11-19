using UnityEngine;
using System.Collections;

public class HealthManBarNPC:MonoBehaviour{
	
	private SpiderState myState;
	public float maxValueHealth = 100;
	public float maxValueMana = 100;
	public float currentHealth = 100;
	public float currentMana = 100;
	public float leftX=0,topY=0,width=100,height=25;
		
	public Texture2D bgBarTexture;
	public Texture2D healthBarFGTexture;
	public Texture2D manaBarFGTexture;
	
	void Awake(){
		myState = this.gameObject.GetComponent<SpiderState>();
		set(myState.getMAXHP(),myState.getMAXMP(),myState.getHP(),myState.getMP(),leftX,topY,width,height);
	}

	public void set(float maxHealth,float maxMana,float currentHealth,float currentMana,
	                float leftX,float topY,float width,float height){
		this.maxValueHealth = maxHealth;
		this.maxValueMana = maxMana;
		this.currentHealth = currentHealth;
		this.currentMana = currentMana;
		this.leftX = leftX;
		this.topY = topY;
		this.width = width;
		this.height = height;
	}
	
	void Update(){
		// TODO ?
		set(myState.getMAXHP(),myState.getMAXMP(),myState.getHP(),myState.getMP(),leftX,topY,width,height);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(myState.transform.position);
		this.leftX = screenPos.x-this.width*0.5f;
		this.topY = Screen.height-screenPos.y-this.height*0.5f-50;
	}
	
	void OnGUI(){
		// BG container
		GUI.DrawTexture(new Rect(this.leftX,this.topY,this.width,this.height),bgBarTexture);

		// health FG container
		GUI.DrawTexture(new Rect(this.leftX,this.topY,(this.currentHealth/this.maxValueHealth)*this.width,this.height*0.5f),healthBarFGTexture);

		// mana FG container
		GUI.DrawTexture(new Rect(this.leftX,this.topY+this.height*0.5f,(this.currentMana/this.maxValueMana)*this.width,this.height*0.5f),manaBarFGTexture);
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
	
	public void setMaxMana(float maxMana){
		this.maxValueMana = maxMana;
	}
	
	public void setLeftX(float leftX){
		this.leftX = leftX;
	}
	
	public void setTopY(float topY){
		this.topY = topY;
	}
	
	public void setWidth(float width){
		this.width = width;
	}
	
	public void setHeight(float height){
		this.height = height;
	}
}
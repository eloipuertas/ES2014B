using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManBarNPC:MonoBehaviour{
	
	private SpiderState myState;
	private SpiderAI ai;
	public float maxValueHealth = 100;
	public float maxValueMana = 100;
	public float currentHealth = 100;
	public float currentMana = 100;
	public float leftX=0,topY=0,width=100,height=25;
		
	public Texture2D bgBarTexture;
	public Texture2D coverGlassFGTexture;

	// animation
	private float timeBetweenAnimation = 0.05f;
	private float timeLeftAnimationChange = 0;
	private bool animationHealthForward;
	private bool animationManaForward;
	private int animationHealthIndex;
	private int animationManaIndex;
	public List<Texture2D> healthBarFGTextures;
	public List<Texture2D> manaBarFGTextures;

	
	void Awake(){
		myState = this.gameObject.GetComponent<SpiderState>();
		ai = this.gameObject.GetComponent<SpiderAI>();
		set(myState.getMAXHP(),myState.getMAXMP(),myState.getHP(),myState.getMP(),leftX,topY,width,height);
		
		
		// animation
		timeLeftAnimationChange = timeBetweenAnimation;
		animationHealthForward = true;
		animationManaForward = true;
		animationHealthIndex = 0;
		animationManaIndex = 0;
	}

	public void set(float maxHealth,float maxMana,float currentHealth,float currentMana,
	                float leftX,float topY,float width,float height){
		this.maxValueHealth = Mathf.Max(0,maxHealth);
		this.maxValueMana = Mathf.Max(0,maxMana);
		this.currentHealth = Mathf.Min(this.maxValueHealth,Mathf.Max(0,currentHealth));
		this.currentMana = Mathf.Min(this.maxValueMana,Mathf.Max(0,currentMana));
		this.leftX = leftX;
		this.topY = topY;
		this.width = width;
		this.height = height;
	}
	
	void Update(){
		if ( isReady() ) {
			// TODO ?
			set(myState.getMAXHP(),myState.getMAXMP(),myState.getHP(),myState.getMP(),leftX,topY,width,height);
			if ( Camera.main != null ) {
				Vector3 screenPos = Camera.main.WorldToScreenPoint(myState.transform.position);
				this.leftX = screenPos.x-this.width*0.5f;
				this.topY = Screen.height-screenPos.y-this.height*0.5f-50;
			}
		}
	}
	
	void OnGUI(){
		if ( isReady() ) {
			// BG container
			GUI.DrawTexture(new Rect(this.leftX,this.topY,this.width,this.height),bgBarTexture);

			// animation
			timeLeftAnimationChange = Mathf.Max(0,timeLeftAnimationChange-Mathf.Abs(Time.deltaTime));
			if(timeLeftAnimationChange <= 0){
				timeLeftAnimationChange = timeBetweenAnimation;
				if(animationHealthForward){
					if(animationHealthIndex < healthBarFGTextures.Count-1){
						animationHealthIndex++;
					}else{
						animationHealthIndex=healthBarFGTextures.Count-2;
						animationHealthForward = false;
					}
				}else{
					if(animationHealthIndex > 0){
						animationHealthIndex--;
					}else{
						animationHealthIndex=1;
						animationHealthForward = true;
					}
				}
				if(animationManaForward){
					if(animationManaIndex < manaBarFGTextures.Count-1){
						animationManaIndex++;
					}else{
						animationManaIndex=manaBarFGTextures.Count-2;
						animationManaForward = false;
					}
				}else{
					if(animationManaIndex > 0){
						animationManaIndex--;
					}else{
						animationManaIndex=1;
						animationManaForward = true;
					}
				}
			}


			// health FG container
			GUI.DrawTexture(new Rect(this.leftX,this.topY,(this.currentHealth/this.maxValueHealth)*this.width,this.height*0.5f),healthBarFGTextures[animationHealthIndex]);
			GUI.DrawTexture(new Rect(this.leftX,this.topY,this.width,this.height*0.5f),coverGlassFGTexture);

			// mana FG container
			GUI.DrawTexture(new Rect(this.leftX,this.topY+this.height*0.5f,(this.currentMana/this.maxValueMana)*this.width,this.height*0.5f),manaBarFGTextures[animationManaIndex]);
			GUI.DrawTexture(new Rect(this.leftX,this.topY+this.height*0.5f,this.width,this.height*0.5f),coverGlassFGTexture);
		}
	}
	
	bool isReady(){
		return myState.isAlive() && ai.currentAction != SpiderAI.PASSIVE;
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
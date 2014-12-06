using UnityEngine;
using System.Collections;

public class TrollState : AbstractEntity {
	public float rotationSpeed = 5f;
	public float timeForNextAction = 0;
	public float timecost_perAction;
	public float timeCostDivisor = 2;
	public float max_attacks_per_second = 5;
	public int maxHPPossible = 2000;
	public int maxMPPossible = 500;
	public float coeff_ConToFor = 1f;
	public float coeff_DexToRef = 0.1f;
	public float coeff_StrToDMG = 10f;
	public float maxPcDMGReduction = 0.90f;

	private Animator animator;

	// Use this for initialization
	void Awake () {
		//animator = GetComponent<Animator>();
		updateStats ();
		InvokeRepeating ("EmulateAttackSpeed", 0, 1f/max_attacks_per_second); 
	}
	private void EmulateAttackSpeed(){ 
		if (timeForNextAction > 0.0) timeForNextAction = timeForNextAction - 1f/max_attacks_per_second;
	}

	public void updateStats(){
		if (STR == 0) setSTR (11);
		else if (STR < 0) setSTR (1);
		else if (STR > 18) setSTR (18);
		if (DEX == 0) setDEX (3);
		else if (DEX < 0) setDEX (1);
		else if (DEX > 18) setDEX (18);
		if (CON == 0) setCON (10);
		else if (CON < 0) setCON (1);
		else if (CON > 18) setCON (18);
		if (INT == 0) setINT (6);
		else if (INT < 0) setINT (1);
		
		setMAXHP (Mathf.RoundToInt (((float)CON/18f) * maxHPPossible));
		setHP (MAXHP);
		setFOR (Mathf.RoundToInt ((float) CON * coeff_ConToFor));
		setREF (Mathf.RoundToInt ((float) DEX * coeff_DexToRef));
		setARM (FOR+REF);
		
		setMAXMP (Mathf.RoundToInt (((float)INT/18f) * maxMPPossible));
		setMP (0);
		setDMG (Mathf.RoundToInt ((float) STR * coeff_StrToDMG));
		
		timecost_perAction = (1f/((float)DEX/18f * max_attacks_per_second));
	}

	public override void onAttackReceived (int baseDMG){
		int damage = Mathf.RoundToInt((1-((float)ARM / 15 * maxPcDMGReduction))*baseDMG);
		this.setHP(HP-damage);
		if (timeCostDivisor > 0 && timeForNextAction<(timecost_perAction/timeCostDivisor)) timeForNextAction = timecost_perAction/timeCostDivisor;
	}

	// LOOK
	public void lookAt(Vector3 lookAtPos){
		if(!Vector3.Equals(lookAtPos, transform.position)){
			Quaternion newRotation = Quaternion.LookRotation(lookAtPos - transform.position);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, (rotationSpeed / 1) * Time.deltaTime);
		}
	}
	// ATTACK
	public void attack(AbstractEntity enemy, Vector3 enemyPos){
		if(this.isAlive() && enemy.isAlive()){
			this.lookAt (enemyPos);
			if (timeForNextAction<=0){
				enemy.onAttackReceived (DMG);
				timeForNextAction = timecost_perAction;
			}
		}
	}

	public void move(Vector3 destiny){

	}
	// Update is called once per frame
	void Update () {
	
	}
}

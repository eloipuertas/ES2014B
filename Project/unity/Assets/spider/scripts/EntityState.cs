using UnityEngine;
using System.Collections;

public class EntityState : MonoBehaviour, IAttacker {
	
	public static int MUERTO = 0;
	public static int VIVO = 1;
	public static int ATACANDO = 2;
	public static int INATACABLE = 3;
	public static int OTRO = 4;

	public int state = VIVO;
	
	public int attack(IAttacker attacker){
		return VIVO;
	}
	
	public int receiveDamage(int damage){
		return VIVO;
	}
	
	public int getState(){
		return state;
	}

}
using UnityEngine;
using System.Collections;

public interface IAttacker {
	int attack(IAttacker attacker);	// return state
	int receiveDamage(int damage);	// return state
	int getState();					// return state
}
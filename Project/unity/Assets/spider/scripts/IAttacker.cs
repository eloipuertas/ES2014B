using UnityEngine;
using System.Collections;

interface IAttacker{
	int attack(IAttacker attacker);	// return state
	int receiveDamage(int damage);	// return state
}
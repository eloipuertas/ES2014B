using UnityEngine;
using System;

public class BasicAI : MonoBehaviour
{
	public Transform target;
	
	public const int PASSIVE = 0;
	public const int MOVING = 1;
	public const int ATTACKING = 2;

	public int currentAction = PASSIVE;
}


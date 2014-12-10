using UnityEngine;
using System.Collections;

public class AISelectable : Selectable {

	void Update()
	{
		BasicAI ai = this.gameObject.GetComponent<BasicAI>();
		AbstractEntity state = this.gameObject.GetComponent<AbstractEntity>();

		if ( state.isAlive() )
		{
			if ( ai.currentAction == BasicAI.PASSIVE )
			{
				outlineColor = Color.blue;
			}
			else
			{
				outlineColor = Color.red;
			}
		}
		else
		{
			outlineColor = Color.gray;
		}
	}
	
}

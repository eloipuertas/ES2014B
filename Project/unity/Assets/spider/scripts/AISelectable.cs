using UnityEngine;
using System.Collections;

public class AISelectable : Selectable {

	void Update()
	{
		BasicAI ai = this.gameObject.GetComponent<BasicAI>();
		SpiderState state = this.gameObject.GetComponent<SpiderState>();

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

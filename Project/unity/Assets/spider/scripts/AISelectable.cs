using UnityEngine;
using System.Collections;

public class AISelectable : Selectable {

	void Update()
	{
		SpiderAI ai = this.gameObject.GetComponent<SpiderAI>();
		SpiderState state = this.gameObject.GetComponent<SpiderState>();

		if ( state.isAlive() )
		{
			if ( ai.currentAction == SpiderAI.PASSIVE )
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

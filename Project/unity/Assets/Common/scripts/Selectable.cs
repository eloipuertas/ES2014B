using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	private Renderer childRenderer;

	public int layer = 0;
	public Color outlineColor = Color.green;
	public Shader validTargetShader;
	public Shader originalShader = null;

	void Awake()
	{
		childRenderer = GetComponentInChildren<Renderer>();
	}
	
	void Update ()
	{
		RaycastHit hit;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if ( Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << layer) )
		{
			Transform target = hit.transform;

			//Debug.Log("ray.origin: " + ray.origin);
			//Debug.Log("ray.direction: " + ray.direction);

			if ( target == transform )
			{
				overrideShader();
			}
			else
			{
				restoreShader();
			}
		}
		else
		{
			restoreShader();
		}
	}

	private void overrideShader()
	{
		if ( childRenderer )
		{
			if ( originalShader == null )
			{
				originalShader = childRenderer.material.shader;
			}

			childRenderer.material.shader = validTargetShader;
			childRenderer.material.SetColor("_OutlineColor", outlineColor);
			
			//Debug.Log("childRenderer.material.shader: " + childRenderer.material.shader);
		}
	}

	private void restoreShader()
	{
		if ( childRenderer && originalShader != null )
		{
			childRenderer.material.shader = originalShader;
			
			//Debug.Log("childRenderer.material.shader: " + childRenderer.material.shader);
		}
	}

}

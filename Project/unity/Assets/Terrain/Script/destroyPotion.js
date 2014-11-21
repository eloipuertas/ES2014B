#pragma strict

function onTriggerEnter(other : Collider)
{
	if (other.tag == "Player"){
		Destroy(gameObject);
	}
}
@script RequireComponent( AudioSource )

var walkSounds:AudioClip[];

// Use this for initialization
function Start () {
}

// Update is called once per frame
function Update () 
{
	if ( transform.hasChanged && ! audio.isPlaying ) 
	{
		audio.clip = walkSounds[Random.Range(0, walkSounds.length)];
		audio.Play();
		
		transform.hasChanged = false;
		
	}
	
}
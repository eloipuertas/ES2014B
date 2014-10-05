@script RequireComponent( AudioSource )

var walkSounds:AudioClip[];

// Use this for initialization
function Start () {
}

// Update is called once per frame
function Update () 
{
	if ( Input.GetAxis( "Horizontal" ) || Input.GetAxis( "Vertical" ) ) 
	{
		audio.clip = walkSounds[Random.Range(0, walkSounds.length)];
		audio.Play();
	}
    else
    {
		audio.Stop();
    }
	
}
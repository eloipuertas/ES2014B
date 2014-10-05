@script RequireComponent( AudioSource )

var walk : AudioClip;

// Use this for initialization
function Start () {
}

// Update is called once per frame
function Update () 
{
	var direction = Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
	
	if(direction.magnitude > 1.0) direction.Normalize();

	if ( direction.magnitude > 0 ) 
	{
		audio.Play();
	}
    else
    {
       audio.Stop();
    }
	
}


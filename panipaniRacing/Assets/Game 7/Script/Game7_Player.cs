using UnityEngine;
using System.Collections;

public class Game7_Player : MonoBehaviour
{
	public GUISkin skin;			//GUI skin
	public GameObject mesh;			//Mesh
	public float speed;				//Move speed
	public AudioClip audioHitHole;	//Hole sound
	public AudioClip audioHitWall;	//Wall sound
	private bool dead;				//Are we dead
	private bool win;				//Has we won
	private bool hitHole;			//Are we in a hole
	private GameObject hole;		//The hole we are in
	
	void Start ()
	{
		//Set screen orientation portrait
		Screen.orientation = ScreenOrientation.Portrait;
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update ()
	{
		//If we are not dead and we are not in a hole and we has not won
		if (!dead && !hitHole && !win)
		{
			//Update
			MoveUpdate();
		}
		//If we are not dead and we are in a hole and we has not won
		else if(!dead && hitHole && !win)
		{
			//Set direction to the hole
			Vector3 dir = transform.position - hole.transform.position;
			//Add force to the ball
			rigidbody.AddForce(-dir * speed * Time.deltaTime);
			
			//Make the ball small
			transform.localScale -= new Vector3(0.1f,0.1f,0.1f) * Time.smoothDeltaTime;
			
			//If the ball scale is less than 0.2
			if (transform.localScale.x < 0.2f)
			{
				//Dont show ball
				mesh.renderer.enabled = false;
				//Kill
				dead = true;
			}
		}
	}
	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//AddForce (x force = Horizontal) (z force = Vertical)
			rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * speed * Time.deltaTime);
		}
		//If the game is running on a android device
		else
		{
			//AddForce (x force = acceleration.y) (z force = acceleration.x)
			rigidbody.AddForce(new Vector3(-Input.acceleration.y,0,Input.acceleration.x) * speed * 5 * Time.deltaTime);
		}	
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are not in a hole and we are in a enemy trigger
		if (!hitHole && other.tag == "Enemy")
		{
			//Play hole sound
			audio.clip = audioHitHole;
			audio.Play();
			//Set hole to the hole we has hit
			hole = other.gameObject;
			//Set hitHole to true
			hitHole = true;
		}
		//If we are in a win trigger
		else if (other.tag == "Win")
		{
			//We won the game
			win = true;
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		//If relative velocity is bigger than 2
        if (collision.relativeVelocity.magnitude > 2)
		{
			//Play wall sound
            audio.clip = audioHitWall;
			audio.Play();
		}
    }
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		
		//If we have won or are dead
		if (win || dead)
		{
			//If we have won
			if (win)
			{
				//Win
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Win");
			}
			//If we are dead
			else if (dead)
			{
				//Dead
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Dead");
			}
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 7");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
}

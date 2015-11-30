using UnityEngine;
using System.Collections;

public class Game4_Player : MonoBehaviour
{
	public GUISkin skin;				//GUI skin
	public float moveSpeed;				//Move speed
	public float rotSpeed;				//Rotate speed
	public GameObject[] checkpoints;	//All checkpoints
	public AudioClip audioCheckpoint;	//Checkpoint sound
	private int rounds = 0;				//Rounds
	private int nextCheckpoint = 1;		//Next checkpoint
	private GameObject rightTouchPad;	//Right touchpad
	private GameObject leftTouchPad;	//Left touchpad
	private bool win;					//Have we won
	
	void Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find left touchpad
		leftTouchPad = GameObject.Find("LeftTouchPad");
		//Find right touchpad
		rightTouchPad = GameObject.Find("RightTouchPad");
		//StartSetupJoysticks
		StartCoroutine("SetupJoysticks");
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update ()
	{
		//Update
		MoveUpdate();
	}
	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//If Horizontal is not 0
			if (Input.GetAxis("Horizontal") != 0)
			{
				//If Vertical is not 0
				if (Input.GetAxis("Vertical") != 0)
				{
					//Rotate the player
					transform.Rotate(0,Input.GetAxis("Horizontal") * rotSpeed * 10 * Time.smoothDeltaTime,0);
				}
			}
			
			//Move the player
			rigidbody.AddForce(transform.forward * -Input.GetAxis("Vertical") * (moveSpeed - 5) * 100 * Time.smoothDeltaTime);
		}
		//If the game is running on a android device
		else
		{
			//Get the left touchpad x positon
			float pX = leftTouchPad.GetComponent<Joystick>().position.x;
			//Get the left touchpad y positon
			float pY = rightTouchPad.GetComponent<Joystick>().position.y;
			
			//If touchpad y positon is not 0
			if (pY != 0)
			{
				//If touchpad x positon is not 0
				if (pX != 0)
				{
					//Rotate the player
					transform.Rotate(0,pX * rotSpeed * 10 * Time.smoothDeltaTime,0);
				}
			}
			
			//Move the player
			rigidbody.AddForce(transform.forward * -pY * moveSpeed * 100 * Time.smoothDeltaTime);
		}
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		//Rounds left
		GUI.Label(new Rect(10,10,300,300),"Rounds: " + rounds + "/3");
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		//If we won
		if (win)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 4");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are in a checkpoint trigger
		if (other.tag == "Checkpoint")
		{
			//If its name is checkpoint + nextCheckpoint
			if (other.name == "checkpoint " + nextCheckpoint.ToString())
			{
				//If its name is checkpoint 0 and nextCheckpoint is 0
				if (other.name == "checkpoint 0" && nextCheckpoint == 0)
				{
					//Add 1 to rounds
					rounds++;
					//If rounds is bigger than 3
					if (rounds >= 3)
					{
						//We won
						win = true;
					}
				}
				//Add 1 to nextCheckpoint
				nextCheckpoint++;
				//Play checkpoint sound
				audio.clip = audioCheckpoint;
				audio.Play();
				//If nextCheckpoint is bigger than checkpoints.Length - 1
				if (nextCheckpoint > checkpoints.Length - 1)
				{
					//Set nextCheckpoint to 0
					nextCheckpoint = 0;
				}
			}
		}
	}
	
	IEnumerator SetupJoysticks()
	{
		//Set position
		leftTouchPad.transform.position = new Vector3(0,0,0);
		rightTouchPad.transform.position = new Vector3(1,0,0);
		
		//Wait 1 second
		yield return new WaitForSeconds(1);
		
		//Start touchpad
		leftTouchPad.GetComponent<Joystick>().StartGame();
		rightTouchPad.GetComponent<Joystick>().StartGame();
	}
}

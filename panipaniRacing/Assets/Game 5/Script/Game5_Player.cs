using UnityEngine;
using System.Collections;

public class Game5_Player : MonoBehaviour
{
	public GUISkin skin;					//GUI skin
	public int balls = 3;					//Balls left
	public GameObject[] tmpBalls;			//Tmp balls
	public GameObject ball;					//The ball we are using
	public GameObject prefabBall;			//Ball prefab
	public Transform ballStartPosition;		//Ball start position
	public GameObject[] ropes;				//Ropes
	public GameObject cameraGo;				//Main camera
	public int maxCameraXPosition;			//Max camera x position
	public int minCameraXPosition;			//Min camera x position
	public AudioClip audioSlingshot;		//Slingshot sound
	private Vector3 touchDownPosition;		//The first touch position
	private Vector3 touchPosition;			//Touch position
	private bool down;						//Is finger on the screen
	private bool moveCamera;				//Is moving camera
	private bool autoMoveCamera;			//Auto move camera
	private Vector2 velocity;				//Velocity
	private int score;						//Score
	private int enemys;						//Enemys
	private bool win;						//Have we won
	private bool dead;						//Are we daed
	
	void Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find enemys
		GameObject[] enemyGo = GameObject.FindGameObjectsWithTag("Enemy");
		//Go through all the enemys
		foreach(GameObject item in enemyGo)
		{
			//Add enemy
			enemys++;
			//Set name
			string a = item.name;
			item.name = a;
		}
		//Set sleep time tp never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		//Set label color to black
		skin.label.normal.textColor = Color.black;
	}

	void Update()
	{
		//If auto move camera
		if (autoMoveCamera)
		{
			//Move camera
			cameraGo.transform.position = new Vector3(Mathf.SmoothDamp(cameraGo.transform.position.x, maxCameraXPosition, ref velocity.x, 0.3f),cameraGo.transform.position.y,cameraGo.transform.position.z);
			if (cameraGo.transform.position.x >= maxCameraXPosition - 0.1f)
			{
				//Set autoMoveCamera to false
				autoMoveCamera = false;
			}
		}
		
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//Set touchPosition to mouse position
			touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,0));
		}
		//If the game is running on a android device
		else
		{
			//Set touchPosition to finger position
			touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
		}
		
		//Set touchPosition z to 0
		touchPosition = new Vector3(touchPosition.x,touchPosition.y,0);
		//Get left mouse button up
		if (Input.GetMouseButtonUp(0))
		{
			//If move camera
			if (moveCamera)
			{
				//Set moveCamera to false 
				moveCamera = false;
			}
			//If finger is on the screen
			if (down)
			{
				//Set down to false
				down = false;
				
				//Make the ball look at the ball start position
				ball.transform.LookAt(ballStartPosition);
				//Set isKinematic to false
				ball.rigidbody.isKinematic = false;
				//Add forward force
				ball.rigidbody.AddForce(ball.transform.forward * (275 * Vector3.Distance(touchPosition,ballStartPosition.position)));
				//Set ball position
				ball.transform.position = ballStartPosition.position;
				
				//Go through all ropes
				foreach (GameObject item in ropes)
				{
					//Set LineRenderer position 1
					item.GetComponent<LineRenderer>().SetPosition(1,ballStartPosition.position);
				}
				//Set autoMoveCamera to true
				autoMoveCamera = true;
				//Set ball to null
				ball = null;
				//Play slingshot sound
				audio.clip = audioSlingshot;
				audio.Play();
				//Start NewBall
				StartCoroutine("NewBall");
			}
		}
		//Get left mouse button down
		if (Input.GetMouseButtonDown(0))
		{
			//Set touchDownPosition z to 0
			touchDownPosition = new Vector3(touchPosition.x,touchPosition.y,0);
			//If ball is not null and the distance between touchPosition and ball position is less than 1
			if (ball != null && Vector3.Distance(touchPosition,ball.transform.position) < 1)
			{
				//Set down to true
				down = true;
			}
			else
			{
				//Set moveCamera to true
				moveCamera = true;
			}
		}
		
		//If finger is on the screen and the distance between touchPosition and touchPosition is less than 5.5
		if (down && Vector3.Distance(touchDownPosition,touchPosition) < 5.5f)
		{
			//Set ball position
			ball.transform.position = touchPosition;
			
			//Go through all ropes
			foreach (GameObject item in ropes)
			{
				//Set LineRenderer position
				item.GetComponent<LineRenderer>().SetPosition(1,ball.transform.position);
			}
		}
		//moveCamera is true and autoMoveCamera is false
		if (moveCamera && !autoMoveCamera)
		{
			//Set a to touchDownPosition.x - touchPosition.x (to see which side we will go)
			//If a is bigger than 0 go left, if it is less than 0 go right
			float a = touchDownPosition.x - touchPosition.x;
			//Get camera position
			float b = cameraGo.transform.position.x;
			//Set c to b + a
			float c = b+a;
			
			//If camera position is bigger than maxCameraXPosition
			if (b >= maxCameraXPosition)
			{
				//If we are trying to go right
				if (c > b)
				{
					return;
				}
			}
			//If camera position is less than minCameraXPosition
			if (b <= minCameraXPosition)
			{
				//If we are trying to go left
				if (c < b)
				{
					return;
				}
			}
			
			//Move camera
			cameraGo.transform.position = new Vector3(c,cameraGo.transform.position.y,cameraGo.transform.position.z);
		}
		
		//If balls is 0
		if (balls == 0)
		{
			//If we cant find any balls
			if (GameObject.Find("Ball") == null && GameObject.Find("Ball(Clone)") == null)
			{
				//If we have not won
				if (!win)
				{
					//Kill
					dead = true;
				}
			}
		}
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),"Score: " + score.ToString());
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		
		//If we are dad or have won
		if (win || dead)
		{
			//If we have won
			if (win)
			{
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Win");
			}
			//If are dead
			else if (dead)
			{
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Dead");
			}
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 5");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
	
	public void AddScore(int _score)
	{
		//Add _score value to score
		score += _score;
	}
	public void RemoveEnemy()
	{
		//Remove 1 enemy
		enemys--;
		//If enemys is 0
		if (enemys == 0)
		{
			//Start Win
			StartCoroutine("Win");
		}
	}
	IEnumerator NewBall()
	{
		//Remove 1 ball
		balls--;
		//Wait 1 second
		yield return new WaitForSeconds(1);
		//If balls is bigger than 0
		if (balls > 0)
		{
			//Instantiate ball
			ball = Instantiate(prefabBall,ballStartPosition.position,ballStartPosition.transform.rotation) as GameObject;
			//Kill tmp balls
			Destroy(tmpBalls[tmpBalls.Length - balls]);
		}
	}
	IEnumerator Win()
	{
		//Wait 2 second
		yield return new WaitForSeconds(2);
		//Set win to trueF
		win = true;
	}
}
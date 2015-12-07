using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Game11_Player : MonoBehaviour
{

	public Text highScoreText;
	public GUISkin skin;							//GUI Skin

	public float speedLeftRight = 2000;				//Move left and right speed
	public float speedForward = 1;				//Move forward speed
	public float speedJump;							//Jump Speed
	//public float gravity = 120;						//Gravity
	private bool dead;								//Are we dead
	private Vector3 moveDirection = Vector3.zero;	//Move Direction
	private float score;							//Score
	private bool isGrounded = true;					//Is grounded
	private bool canJump = true;					//Can jump
	private bool canTurn;							//Can turn
	private bool canMoveLeft;						//Can move left
	private bool canMoveRight;						//Can move right
	public GameObject goCamera;					//Main Camera
	private int dir;								//Move direction (the way we are looking)
	private Vector2 startTouchPos;					//The first position we touch

	public GameObject scoreImage;
	public GameObject HeightObj;
	public GameObject coinText;

	public GameObject soundStuff;

	private int CoinNum;

	Text highText;

	public GameObject topScoreObj;




	public simpleAds admanager;

	void Start ()
	{

		CoinNum =0;
		highText = HeightObj.GetComponent<Text>();

		//Set the screen orientation to portrait
		//Screen.orientation = ScreenOrientation.Portrait;
		//Set the sleep time to nerver

		Screen.SetResolution ( 960,640, true, 60);

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		//Find Camera
		goCamera = GameObject.Find("Camera");

		admanager = GameObject.Find("AdManager").GetComponent<simpleAds>();
	}
	
	void Update()
	{
		//If we are not dead
		if (!dead)
		{
			//Update
			MoveUpdate();
			//Add 5 to the score every second
			score += 5 * Time.deltaTime;
		}
		
		//If we are hitting a object (down)
		RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,1))
		{
			//If it is not the player
            if (hit.transform.gameObject.tag != "Horse_Prefab")
			{
				//We are grounded
            	isGrounded = true;
            }
		}
		else
		{
			//Debug.Log("is grond false");
			//We are in the air
			isGrounded = false;
		}
		
		//If we are hitting a object (right)
		RaycastHit hitRight;
        if (Physics.Raycast(transform.position, transform.right, out hitRight,1))
		{
			//If it is a wall
            if (hitRight.transform.gameObject.name == "Wall")
			{
				//We can not move right
            	canMoveRight = false;
            }
		}
		else
		{
			//We can move right
			canMoveRight = true;
		}
		
		//If we are hitting a object (left)
		RaycastHit hitLeft;
        if (Physics.Raycast(transform.position, - transform.right, out hitLeft,1))
		{
			//If it is a wall
            if (hitLeft.transform.gameObject.name == "Wall")
			{
				//We can not move left
            	canMoveLeft = false;
            }
		}
		else
		{
			//We can move left
			canMoveLeft = true;
		}
	}// end move update


	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//The moveDirection x is the horizontal axis (A,D)
			moveDirection.x = Input.GetAxis("Horizontal");
			
			//If moveDirection x is bigger than 0.3 and we can move right
			if (moveDirection.x > 0.3f && canMoveRight)
			{
				//Set moveDirection x to 1
				moveDirection.x = 1f;
			}
			//If moveDirection x is less than -0.3 and we can move left
			else if (moveDirection.x < -0.3f && canMoveLeft)
			{
				//Set moveDirection x to -1
				moveDirection.x = -1f;
			}
			else
			{
				//Set moveDirection x to 0
				moveDirection.x = 0;
			}
			//If get Space key down and we can jump and is on the ground
			if (Input.GetKeyDown(KeyCode.Space) && canJump )
			{
				rigidbody.AddForce(Vector3.up * 150 * speedJump);
				soundStuff.GetComponent<SoundFX>().playJumpSound();
				Debug.Log(""+isGrounded);

				//Set canJump to false
				canJump = false;
				//Add up force

				animation.Play ("horse_jump");
				new WaitForSeconds(1);
				animation.Play ("horse_galloping");
				//Start WaitToJump
				StartCoroutine("WaitToJump");

			}
			//If can turn and get E key down
			if (canTurn && Input.GetKeyDown(KeyCode.E))
			{
				//Rotate right
				Rotate("Right");
			}
			//If can turn and get E key down
			if (canTurn && Input.GetKeyDown(KeyCode.Q))
			{
				//Rotate left
				Rotate("Left");
			}
		}
//If the game is running on a android device------------------------------------------------------------------
		else if(Application.platform == RuntimePlatform.Android ||
		        Application.platform == RuntimePlatform.IPhonePlayer)
		{
			//moveDirection x = the phone tilt value
			moveDirection.x =  Input.acceleration.x; //y
			//If moveDirection x is bigger than 0.15 and we can move right
			if (moveDirection.x > 0.05f && canMoveRight)
			{
				//Set moveDirection x to 1
				moveDirection.x = 1.0f;
			}
			//If moveDirection x is less than -0.15 and we can move left
			else if (moveDirection.x < -0.05f && canMoveLeft)
			{
				//Set moveDirection x to -1
				moveDirection.x = -1.0f;
			}
			else
			{
				//Set moveDirection x to 0
				moveDirection.x = 0;
			}
			
			//Get touches
			foreach (Touch touch in Input.touches)
			{
				//Touche phase = began
				if (touch.phase == TouchPhase.Began)
				{
					//Set first touch position
					startTouchPos = touch.position;


					if (canJump)
					{

						soundStuff.GetComponent<SoundFX>().playJumpSound();
						//Add up force
						animation.Play ("horse_jump");
						rigidbody.AddForce(Vector3.up * 150 * speedJump);
						//Set canJump to false
						canJump = false;
						
						//Start WaitToJump
						new WaitForSeconds(1);
						animation.Play ("horse_galloping");
						StartCoroutine("WaitToJump");
					}


				}
				//Touche phase = moved
	            if (touch.phase == TouchPhase.Moved)
				{
					//If we can jump and is grounded and touch position y is bigger than first touch position y + 100
					if (canJump && touch.position.y-startTouchPos.y > 50)
					{
						soundStuff.GetComponent<SoundFX>().playJumpSound();
						//Add up force
						animation.Play ("horse_jump");
						rigidbody.AddForce(Vector3.up * 150 * speedJump);
						//Set canJump to false
						canJump = false;

						//Start WaitToJump
						 new WaitForSeconds(1);
		            animation.Play ("horse_galloping");
						StartCoroutine("WaitToJump");
					}
					//If we can turn and touch position x is bigger than first touch position x + 100
					if (canTurn && touch.position.x-startTouchPos.x  >  100)
					{
						//Rotate right
						Rotate("Right");
					}
					//If we can turn and touch position x is less than first touch position x - 100
					if (canTurn && touch.position.x - startTouchPos.x < - 100)
					{
						//Rotate left
						Rotate("Left");
					}
				}
			}
		}
		
//Move the player

//move speed by score

		if(50 < score && score < 500){

			speedForward =20;
		}

		if(1000>score && score >500){
			speedForward = 25;

		}

		if(1000<score && score< 3000){

			speedForward = 30;
		}

		if( score > 4000){
			
			speedForward = 35;
		}



		transform.Translate(new Vector3(moveDirection.x * speedLeftRight,0,speedForward) * Time.deltaTime,Space.Self);

		if(dead ==false){


			//GameObject.Find("Canvas").transform.Find("PlayButton").active = false;

		}


	}//update end
//triger

	void OnTriggerEnter(Collider other)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			animation.Play("horse_running_death");
			soundStuff.GetComponent<SoundFX>().horseNighPlay();
			//Kill
			dead = true;
			int died = PlayerPrefs.GetInt("died");
			PlayerPrefs.SetInt("died",died+1);

//			Debug.Log("died: "+died);
			if(died  % 7 ==6 ){

			admanager.ShowAd();
			}

		}
		//If we are in a turning trigger
		if (other.tag == "Turning")
		{
			//We can turn
			canTurn = true;
		}
		//If we are in a coin trigger
		if (other.tag == "Coin")
		{
			
			GameObject.Find("SoundStuff").GetComponent<SoundFX>().coinSound();

			//Add 10 to score
			score += 10;
			//Destroy coin
			Destroy(other.gameObject);

			CoinNum++;

			int currentCoin = PlayerPrefs.GetInt("user_coin");
			int wholecoin = CoinNum+currentCoin;
			PlayerPrefs.SetInt("user_coin", wholecoin);

			coinText.GetComponent<Text>().text ="coin: "+ wholecoin;

		}

		if(other.tag=="lastblock"){

			goCamera.GetComponent<Game11_InstantiateLevel>().SpawnPlatform("forward");
			//soundStuff.GetComponent<SoundFX>().explodeAudio();


		}

		if(other.tag=="chunkwall"){

			soundStuff.GetComponent<SoundFX>().explodeAudio();

		}//wall
	}
	void OnTriggerExit(Collider other)
	{
		//If we are not in a turning trigger
		if (other.tag == "Turning")
		{
			//We cant turn
			canTurn = false;

		}
	}
//gui	
	void OnGUI()
	{
		Text scoretext = scoreImage.GetComponent<Text>();



		//If we are dead*/
		if (dead)
		{

			GameObject.Find("Canvas").transform.Find("PlayButton").active = true;


			int highestScore = PlayerPrefs.GetInt("highest_score");
			int intscore = (int) score;

			if(highestScore < intscore){

				PlayerPrefs.SetInt("highest_score", intscore);

			}
			GameObject.Find("Canvas").transform.Find("PlayButton").transform.Find("topscore").GetComponent<Text>().text ="new highest score: "+ PlayerPrefs.GetInt("highest_score");

		}//end if	


		scoretext.text = "score: "+ ((int)score).ToString();
		
		
		highText.text = "speed: "+speedForward;


	}

	public void PlayAgain(){
		Time.timeScale = 1;
		Application.LoadLevel("Game 11");

		//
		GameObject.Find("PlayButton").active =  false;
	}
	
	IEnumerator WaitToJump()
	{
		//Wait 1 second
		yield return new WaitForSeconds(0.1f);

		animation.Play("horse_rear_up");
		//We can jump
		canJump = true;
	}
//rotate	
	void Rotate(string _dir)
	{
		//We can not rotate
		canTurn = false;
		//If the direction we are looking is left
		if (_dir == "Left")
		{
			//Add -90 to eulerAngles
			transform.eulerAngles += new Vector3(0,-90,0);
		}
		//If the direction we are looking is right
		else
		{
			//Add 90 to eulerAngles
			transform.eulerAngles += new Vector3(0,90,0);
		}
		
		//Spawn new platforms
		goCamera.GetComponent<Game11_InstantiateLevel>().SpawnPlatform(_dir);
		
		//If the direction we are looking is left
		if (_dir == "Left")
		{
			//Remove 1
			dir--;
		}
		//If the direction we are looking is right
		else if (_dir == "Right")
		{
			//Add 1
			dir++;
		}
		//If dir is bigger than 3
		if (dir > 3)
		{
			//Set dir to 0
			dir = 0;
		}
		//If dir is less than 0
		else if (dir < 0)
		{
			//Set dir to 0
			dir = 3;
		}
	}
}
using UnityEngine;
using System.Collections;

public class CsHuman : MonoBehaviour {
	public GUISkin skin;
	GameObject manager;

	int speedForward = 12;
	int speedSide = 6;
	int jumpPower = 250;

	bool canJump = true;
	bool canTurn = false;
	bool canLeft = false;
	bool canRight = true;
	bool isGround = true;
	bool isDead = false;

	float dirX = 0;
	float score = 0;

	Vector3 touchStart;

	CsFollowCam _folloCam;

	public GameObject appleString;
	public GameObject bananaString;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;


		manager = GameObject.Find ("BridgeManager");
		_folloCam = GameObject.Find ("Main Camera").GetComponent<CsFollowCam>();
		_folloCam.setTarget (transform);
		appleString.SetActive (false);
		bananaString.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameState.isStart)return;
		if (isDead) return;
		if (GameState.isWin) return;
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}

		CheckMove();
		MoveHuman();

		score += Time.deltaTime * 1000;
	}

	void CheckMove(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 2f)) {
			if(hit.transform.tag == "BRIDGE"){
				isGround = true;
			}
		}

		canLeft = true;
		if (Physics.Raycast (transform.position, -transform.right, out hit, 0.7f)) {
			if(hit.transform.tag == "GUARD"){
				canLeft = false;
			}
		}

		canRight = true;
		if (Physics.Raycast (transform.position, transform.right, out hit, 0.7f)) {
			if(hit.transform.tag == "GUARD"){
				canRight = false;
			}
		}
	}

	void MoveHuman(){
		dirX = 0;
		if (Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer) {
			//CheckMobile();
		} else {
			CheckKeyboard();
		}

		Vector3 moveDir = new Vector3 (dirX * speedSide, 0, speedForward);
		transform.Translate (moveDir * Time.smoothDeltaTime);
	}

	void CheckMobile(){
		float x = Input.acceleration.x;

		if (x < -0.2f && canLeft && isGround)
			dirX = -0.6f;
		if (x > 0.2f && canRight && isGround)
			dirX = 0.6f;

		foreach (Touch tmp in Input.touches) {
			if(tmp.phase == TouchPhase.Began){
				touchStart = tmp.position;
			}

			if(tmp.phase == TouchPhase.Moved){
				Vector3 touchEnd = tmp.position;

				//아래에서 위로 드래그한경우
				if(isGround && canJump && touchEnd.y - touchStart.y>100){
					StartCoroutine("JumpHuman");
				}

				if(isGround && touchEnd.x - touchStart.x<-100){
					RotateHuman("LEFT");
				}

				if(isGround && touchEnd.x - touchStart.x>100){
					RotateHuman("RIGHT");
				}
			}

		}
	}

	void CheckKeyboard(){
		float key = Input.GetAxis ("Horizontal");

		if (key < 0 && canLeft && isGround) dirX = -1;
		if (key > 0 && canRight && isGround) dirX = 1;

		if (isGround && canJump && Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine("JumpHuman");
		}

		if (canTurn) {
			if(Input.GetKeyDown(KeyCode.Q)) RotateHuman("LEFT");
			if(Input.GetKeyDown(KeyCode.E)) RotateHuman("RIGHT");
		}
	}

	IEnumerator JumpHuman(){
		canJump = false;
		transform.rigidbody.AddForce (Vector3.up * jumpPower);
		animation.Play ("jump_pose");

		yield return new WaitForSeconds(1);
		animation.Play ("run");
		canJump = true;
	}

	void RotateHuman(string sDir){
		canTurn = false;

		Vector3 rot = transform.eulerAngles;

		switch (sDir) {
		case "LEFT":
			StartCoroutine(rotateSmoothly(-90));
			break;
		case "RIGHT":
			StartCoroutine(rotateSmoothly(90));
			break;
		}

		transform.eulerAngles = rot;

		manager.SendMessage ("MakeBridge",sDir,SendMessageOptions.DontRequireReceiver);
	}

	IEnumerator rotateSmoothly(float _angle){
		float countAngle=0.0f;
		float unit = 30.0f;
		Vector3 rot = transform.eulerAngles;
		
		if (_angle < 0)	unit = -unit;
		
		while(true){
			yield return new WaitForSeconds(0.1f);
			rot.y = unit;
			countAngle+=unit;
			if(_angle>0 && countAngle>_angle)
				break;
			if(_angle<0 && countAngle<_angle)
				break;
			
			transform.eulerAngles += rot;
			
		}
		yield return null;
	}

	void OnCollisionEnter(Collision coll){

		if (coll.transform.tag == "DEAD") {
			StartCoroutine(feltPanelty());
		}
	}

	IEnumerator feltPanelty(){
		animation.Play("idle");
		isDead = true;
		yield return new WaitForSeconds (2.0f);
		animation.Play("run");
		isDead = false;
	}

	void OnTriggerEnter(Collider coll){
		switch (coll.transform.tag) {
		case "TURN":
			if(Random.Range(1,3)==1)
				RotateHuman("LEFT");
			else
				RotateHuman("RIGHT");
			//canTurn = true;
			break;
		case "COIN":
			score +=1000;
			Destroy(coll.gameObject);
			break;
		case "APPLE":
			Destroy(coll.gameObject);
			StartCoroutine(VisibleAppleString());
			break;
		case "BANANA":
			Destroy(coll.gameObject);
			StartCoroutine(VisibleBananaString());
			break;
		case "GOAL":
			GameState.isWin = true;
			manager.GetComponent<UIctrl>().openEndScene();
			break;
		}
	}

	IEnumerator VisibleAppleString(){
		appleString.SetActive(true);
		yield return new WaitForSeconds (1.0f);
		appleString.SetActive(false);		
	}

	IEnumerator VisibleBananaString(){
		bananaString.SetActive(true);
		yield return new WaitForSeconds (1.0f);
		bananaString.SetActive(false);		
	}

	void OnTriggerExit(Collider coll){
		if (coll.tag == "TURN") {
			canTurn = false;
		}
	}

	void OnGUI(){
		string str = "<size=20><color=#ffffff>Score : ##</color></size>";
		GUI.Label(new Rect(10,10,300,80),str.Replace("##",""+(int)score));
	}
}

using UnityEngine;
using System.Collections;

public class UIctrl : MonoBehaviour {
	GameObject _CharacterSelectScene;
	GameObject _menuScene;
	GameObject _endScene;
	public float CSoriginPos,CSmovedPos;
	public float MENUoriginPos,MENUmovedPos;
	public float ENDoriginPos,ENDmovedPos;

	public GameObject player;

	// Use this for initialization
	void Start () {
		_CharacterSelectScene = GameObject.Find ("Canvas/CharacterSelectScene");
		_menuScene = GameObject.Find ("Canvas/MenuScene");
		_endScene = GameObject.Find ("Canvas/EndScene");

		CSoriginPos =_CharacterSelectScene.transform.localPosition.x;
		CSmovedPos = -CSoriginPos-1000.0f;

		MENUoriginPos =_menuScene.transform.localPosition.x;
		MENUmovedPos = -MENUoriginPos-1000.0f;
				
		ENDoriginPos =_endScene.transform.localPosition.x;
		ENDmovedPos = -ENDoriginPos-1000.0f;

		Vector3 temp = _CharacterSelectScene.transform.localPosition;
		temp.x = CSmovedPos;
		_CharacterSelectScene.transform.localPosition = temp;

		temp = _endScene.transform.localPosition;
		temp.x = ENDmovedPos;
		_endScene.transform.localPosition = temp;

		GameObject a = (GameObject)Resources.Load("Horse_Prefab");
		player= (GameObject)Instantiate(a,new Vector3(0,1,0),Quaternion.identity);
	}

	public void openEndScene(){
		Vector3 temp = _endScene.transform.localPosition;
		temp.x = ENDoriginPos;
		_endScene.transform.localPosition = temp;
	}

	public void clickCSSbutton(){
		GameState.canStart = false;
		Vector3 temp = _CharacterSelectScene.transform.localPosition;
		temp.x = CSoriginPos;
		_CharacterSelectScene.transform.localPosition = temp;
	}

	public void clickBACKbutton(){
		Vector3 temp = _CharacterSelectScene.transform.localPosition;
		temp.x = CSmovedPos;
		_CharacterSelectScene.transform.localPosition = temp;
		StartCoroutine (changeCanStart());
	}

	IEnumerator changeCanStart(){
		yield return new WaitForSeconds (1.5f);
		GameState.canStart = true;
	}

	public void clickSTARTbutton(){
		Vector3 temp = _menuScene.transform.localPosition;
		temp.x = MENUmovedPos;
		_menuScene.transform.localPosition = temp;
	}

	public void clickHuman(){
		GameObject.Find ("Main Camera").transform.parent = GameObject.Find("temp").transform;
		Destroy (player.gameObject, 0.0f);

		GameObject a = (GameObject)Resources.Load("Human");
		player= (GameObject)Instantiate(a,new Vector3(0,0,0),Quaternion.identity);
	}

	public void clickPangpang(){
		GameObject.Find ("Main Camera").transform.parent = GameObject.Find("temp").transform;
		Destroy (player.gameObject, 0.0f);

		GameObject a = (GameObject)Resources.Load("Pangpang");
		player= (GameObject)Instantiate(a,new Vector3(0,0,0),Quaternion.identity);
	}

	public void clickRestart(){
		Application.LoadLevel("MainGame");
	}

	// Update is called once per frame
	void Update () {

	}
}
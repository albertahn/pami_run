using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameCtrl : MonoBehaviour {
	UIctrl _uiCtrl;
	int Mask;
	// Use this for initialization
	void Start () {

		_uiCtrl = GameObject.Find ("BridgeManager").GetComponent<UIctrl>();
		GameState.isStart = false;
		GameState.canStart = true;
		GameState.isWin=false;

	}//end
	
	// Update is called once per frame
	void Update () {

			if (Input.GetMouseButtonDown (0)){

				StartCoroutine (StartGame ());

			}
	}

	IEnumerator StartGame(){

		yield return new WaitForSeconds (1.0f);
		if ((!GameState.isStart) && GameState.canStart) {

			GameState.isStart = true;
			_uiCtrl.clickSTARTbutton ();

		}//end
	}

	public void restartGame(){

		Application.LoadLevel(Application.loadedLevel);

	}//re
}

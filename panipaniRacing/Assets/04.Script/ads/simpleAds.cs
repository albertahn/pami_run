using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
public class simpleAds : MonoBehaviour {
	bool showadbool = false;
	// Use this for initialization
	void Start () {
	
		Advertisement.Initialize("1019217",false); //test true


	}//start


	IEnumerator ShowAdWhenReady(){

		Debug.Log("showads");

		while(!Advertisement.isReady()){


			yield return null;

		}//end while

		Advertisement.Show();



	}//ire
	/*
	void Update(){

		int dienum= PlayerPrefs.GetInt("died");

		if(dienum % 3 ==2 ){


			showadbool = true;

		}else{

			Debug.Log("not 2 and");
		}

		if(showadbool==true ){

			StartCoroutine(ShowAdWhenReady());
			showadbool = false;
		}

	}//update*/

	public void ShowAd(){

		StartCoroutine(ShowAdWhenReady());


	}



}//end

using UnityEngine;
using System.Collections;

public class CsBridge : MonoBehaviour {
	public GameObject[] bridges;
	public GameObject[] coins;

	public GameObject apple;
	public GameObject banana;

	public GameObject bridgeTurn;
	public GameObject bridgeGoal;


	GameObject newBridge;
	GameObject childBridge;
	public GameObject oldBridge;

	GameObject bridge;
	GameObject coin;
	bool canCoin = false;

	int dir = 0;
	Quaternion quatAng;

	public int countBridge;

	// Use this for initialization
	void Start () {
		countBridge = 0;
		newBridge = GameObject.Find ("StartBridge");
		oldBridge = GameObject.Find ("OldBridge");
		childBridge = newBridge;

		MakeBridge ("FORWARD");	
	}

	void MakeBridge(string sDir){
		countBridge++;
		DeleteBridge ();
		CalcRotation (sDir);
		MakeNewBridge ();
	}

	void DeleteBridge(){
		Destroy (oldBridge);
		oldBridge = newBridge;

		newBridge = new GameObject ("StartBridge");
	}

	void CalcRotation(string sDir){
		switch (sDir) {
			case "LEFT" : dir--;break;
			case "RIGHT" : dir++;break;
		}

		if (dir < 0) dir = 3;
		if (dir > 3) dir = 0;
		quatAng.eulerAngles = new Vector3 (0, dir * 90, 0);
	}

	void MakeNewBridge(){

		for (int i=0; i<10; i++) {
			bridge = bridges[0];
			coin = coins[Random.Range(0,3)];
			canCoin = false;
			SelectBridge(i);
			Vector3 pos = Vector3.zero;
			Vector3 localPos = childBridge.transform.localPosition;

			switch(dir){
			case 0:
				pos = new Vector3(localPos.x,0,localPos.z+10);
				break;
			case 1:
				pos = new Vector3(localPos.x+10,0,localPos.z);
				break;
			case 2:
				pos = new Vector3(localPos.x,0,localPos.z-10);
				break;
			case 3:
				pos = new Vector3(localPos.x-10,0,localPos.z);
				break;
			}

			childBridge = Instantiate(bridge,pos,quatAng) as GameObject;
			childBridge.transform.parent = newBridge.transform;

			if(canCoin){

				childBridge = Instantiate(coin,pos,quatAng) as GameObject;
				childBridge.transform.parent = newBridge.transform;
			}

			if(canCoin){
				if(Random.Range(1,3)==1){
					childBridge = Instantiate(apple,pos,quatAng) as GameObject;
				}else{
					childBridge = Instantiate(banana,pos,quatAng) as GameObject;
				}
				childBridge.transform.parent = newBridge.transform;
			}

		}
	}

	void SelectBridge(int n){
		switch (n) {
			case 9:
				if(countBridge<2)
					bridge = bridgeTurn;
				else
					bridge = bridgeGoal;
				break;
			case 1:
			case 3:
			case 5:
			case 7:
				bridge = bridges[Random.Range(0,bridges.Length)];
				break;
			default:
				if(Random.Range(0,100)>50){
					canCoin = true;
				}
				break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

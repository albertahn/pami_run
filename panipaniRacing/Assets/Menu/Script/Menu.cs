using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public GUISkin skin;
	private bool loading;
	private Vector2 scrollPosition;
	
	void Start ()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if(GUI.Button(new Rect(Screen.width / 2 - 75,70,150,50),"Game 1"))
		{
			loading = true;
			Application.LoadLevel("Game 1");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,130,150,50),"Game 2"))
		{
			loading = true;
			Application.LoadLevel("Game 2");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,190,150,50),"Game 3"))
		{
			loading = true;
			Application.LoadLevel("Game 3");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,250,150,50),"Game 4"))
		{
			loading = true;
			Application.LoadLevel("Game 4");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,310,150,50),"Game 5"))
		{
			loading = true;
			Application.LoadLevel("Game 5");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,370,150,50),"Game 6"))
		{
			loading = true;
			Application.LoadLevel("Game 6");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,430,150,50),"Game 7"))
		{
			loading = true;
			Application.LoadLevel("Game 7");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,490,150,50),"Game 8"))
		{
			loading = true;
			Application.LoadLevel("Game 8");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,550,150,50),"Game 9"))
		{
			loading = true;
			Application.LoadLevel("Game 9");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,610,150,50),"Game 10"))
		{
			loading = true;
			Application.LoadLevel("Game 10");
		}
		if(GUI.Button(new Rect(Screen.width / 2 - 75,670,150,50),"Game 11"))
		{
			loading = true;
			Application.LoadLevel("Game 11");
		}
		
		if (loading)
		{
			GUI.Label(new Rect(Screen.width / 2 - 45,10,100,100), "Loading");
		}
	}
}
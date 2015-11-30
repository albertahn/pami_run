using UnityEngine;
using System.Collections;

public class Game9_Cloud : MonoBehaviour
{
	public float scrollSpeed = 0.5F;	//The speed the of the texture offset
	
    void Update()
	{
		//Make it smooth
        float offset = Time.time * scrollSpeed;
		
		//Set the texture offset
        renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}

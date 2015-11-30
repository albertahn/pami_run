using UnityEngine;
using System.Collections;

public class Game9_Explosion : MonoBehaviour
{
	public GameObject main;					//Main camera
	public float speed;						//Time it takes to change texture
	private float tmpSpeed;					//Tmp time
	public float offset;					//Offset
	public int index = 0;					//The texture to show
	public int maxIndex;					//Max texture
    public string textureName = "_MainTex";	//Material main texture

	
    void LateUpdate() 
    {
		//If tmpSpeed is bigger than speed
		if (tmpSpeed >= speed)
		{
			//If index is the same as maxIndex
			if (index == maxIndex)
			{
				Destroy(main);
			}
			//Set tmpSpeed to 0
			tmpSpeed = 0;
			//Set texture offset
			renderer.material.SetTextureOffset(textureName,new Vector2(index * offset,0));
			//Add 1 to index
			index++;			
		}
		//If tmpSpeed is less than speed
		else
		{
			//Add 1 to tmpSpeed
			tmpSpeed = tmpSpeed + 1 *Time.deltaTime;	
		}
    }
}

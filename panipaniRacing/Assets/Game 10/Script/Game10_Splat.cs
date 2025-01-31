using UnityEngine;
using System.Collections;

public class Game10_Splat : MonoBehaviour
{
	public GameObject mesh; 			//Mesh
    private Color color;				//Color
    public float destroySpeed = 0.2f;	//Destroy Speed
	
	void Start()
	{
		//Set the color
		color = mesh.renderer.material.color;
	}
	
    void Update()
	{
		//Set the mesh material color
		mesh.renderer.material.color = new Color(color.r,color.g,color.b,color.a -= destroySpeed * Time.deltaTime);
		//If color a is 0
		if (color.a <= 0)
		{
			//Destroy
			Destroy(gameObject);
		}
    }
}

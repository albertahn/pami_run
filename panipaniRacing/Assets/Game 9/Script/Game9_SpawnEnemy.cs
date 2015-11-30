using UnityEngine;
using System.Collections;

public class Game9_SpawnEnemy : MonoBehaviour
{
	public float spawnSpeed;	//Spawn speed
	public GameObject[] enemys;	//Enemy prefabs
	
	void Start()
	{
		//Start Spawn
		StartCoroutine("Spawn");
	}
	
	IEnumerator Spawn()
	{
		//Wait
        yield return new WaitForSeconds(spawnSpeed);
		
		//Spawn enemy
		Instantiate(enemys[Random.Range(0,enemys.Length)],new Vector3(Random.Range(-10,11) * 0.2f,transform.position.y,1),Quaternion.identity);
		
		//Start Spawn
		StartCoroutine("Spawn");
    }
}

using UnityEngine;
using System.Collections;

public class StingerShip : MonoBehaviour 
{
	public int speed;
	float spawnRate;
	float nextStingerTime;
	
	public Rigidbody stinger;
	public Rigidbody stingerClone;
	

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Mathf.FloorToInt(Time.timeSinceLevelLoad) <= 5)
		{
			if(nextStingerTime < Time.time)
			{
				FireStinger();
				nextStingerTime = Time.time + spawnRate;
				spawnRate *= 0.8f;
				spawnRate = Random.Range(0.8f, 1.3f);
			}
		}
	
	}
	
	void FireStinger()
	{
		stingerClone = (Rigidbody) Instantiate(stinger, transform.position, transform.rotation);
		stingerClone.velocity = transform.forward * speed;
	}
}

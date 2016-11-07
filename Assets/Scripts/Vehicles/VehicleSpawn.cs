using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleSpawn : MonoBehaviour 
{
	public Rigidbody chopper;
	public Rigidbody jet;

	float nextVehicleTime = 1.0f;
	float spawnRate = 6;
	
	public int activeAtLevel;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( LevelManager.Instance.CurrentLevel > activeAtLevel && !LevelManager.Instance.IsNextLevel && !GameManager.IsGamePaused)
		{
			if ( nextVehicleTime < Time.time)
			{
				spawnVehicles();
				nextVehicleTime = Time.time + spawnRate;
				spawnRate *= 0.98f;
				spawnRate = Random.Range( 15 , 25 );
			}
		}
	}
	
	public void spawnVehicles()
	{
		Rigidbody[] vehicles = new Rigidbody[2];
		
		vehicles[0] = chopper;
		vehicles[1] = jet;
		
		int vehicleIndex = Random.Range(0, vehicles.Length);
		Instantiate(vehicles[vehicleIndex], transform.position, transform.rotation);
	}
}

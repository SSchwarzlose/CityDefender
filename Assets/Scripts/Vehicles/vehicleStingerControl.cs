using UnityEngine;
using System.Collections;

public class vehicleStingerControl : MonoBehaviour 
{

		public Rigidbody stinger;
	public Rigidbody stingerClone;
	
	public float speed;
	public int delay;
	float nextStingerTime = 1.0f;
	float spawnRate = 3;
	
	public int activeAtLevel;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		if ( Mathf.FloorToInt(Time.timeSinceLevelLoad) > delay && LevelManager.Instance.CurrentLevel >= activeAtLevel &&
			 LevelManager.Instance.StingerLaunched < LevelManager.Instance.StingerValue)
		{
			if ( nextStingerTime < Time.time && LevelManager.Instance.Stingers > 0)
			{
				
				Invoke("FireStinger", 1);
				LevelManager.Instance.TotalStingerLaunched();
				nextStingerTime = Time.time + spawnRate;
				
				spawnRate = Random.Range( 2 , 4 );
			}
		}
		
		
	}
	
	void FireStinger()
	{
		stingerClone = (Rigidbody) Instantiate(stinger, transform.position, transform.rotation);
		stingerClone.velocity = transform.forward * speed;
//		LevelManager.Instance.stinger--;
		
	}
}

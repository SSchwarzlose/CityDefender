using UnityEngine;
using System.Collections;

public class Stinger : MonoBehaviour 
{
	public Rigidbody stinger;
	public Rigidbody stingerClone;
	
	float speed = 75;
	public int delay;
	float nextStingerTime = 1.0f;
	float spawnRate = 3;
	private int i = 1;
	public int activeAtLevel;
	
	// Use this for initialization
	void Start () 
	{
		if (GameManager.Difficulty == 1)
		{
			speed = 75;
		}else
			if (GameManager.Difficulty == 2)
		{
			speed = 100;
		}else
			if (GameManager.Difficulty == 3)
		{
			speed = 125;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Mathf.FloorToInt(Time.timeSinceLevelLoad) > delay && LevelManager.Instance.CurrentLevel >= activeAtLevel &&
			 LevelManager.Instance.StingerLaunched < LevelManager.Instance.StingerValue)
		{
			if ( nextStingerTime < Time.time && LevelManager.Instance.Stingers > 0)
			{
				FireStinger();
				LevelManager.Instance.TotalStingerLaunched();
				nextStingerTime = Time.time + spawnRate;
				spawnRate *= 0.98f;
				spawnRate = Random.Range( 3 , 6 );
			}
		}
		
		if (i == 1 && LevelManager.Instance.CurrentLevel == 3)
		{
			speed *= 1.25f;
			i++;
		}
		
		if (i == 2 && LevelManager.Instance.CurrentLevel == 16)
		{
			speed *= 1.25f;
			i++;
		}
		
		if (i == 3 && LevelManager.Instance.CurrentLevel == 25)
		{
			speed *= 1.25f;
			i++;
		}
		
		if (i == 4 && LevelManager.Instance.CurrentLevel == 40)
		{
			speed *= 1.25f;
			i++;
		}
	}
	
	void FireStinger()
	{
		stingerClone = (Rigidbody) Instantiate(stinger, transform.position, transform.rotation);
		stingerClone.velocity = transform.forward * speed;
		//LevelManager.Instance.stinger--;
	}
}

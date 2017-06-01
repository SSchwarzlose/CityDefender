using UnityEngine;
using System.Collections;

public class stingerControl : MonoBehaviour 
{
	int count = 2;
	float down;
	
	private float nextStingerTime = 1.0f;
	private float spawnRate = 3;
	
	public int countMax;
	int countDown;
	public int delay;
	
	void Update () 
	{
		Targeting();
	}
	
	void Targeting()
	{
		down -= Time.deltaTime;
		if ( down <= 0)
		{
			int Index = Random.Range( 0, LevelManager.Instance.Cities.Length - 1);
			transform.LookAt(LevelManager.Instance.Cities[Index].transform.position);
			down = count;
		}
	}
}

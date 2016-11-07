using UnityEngine;
using System.Collections;

public class Sidewinder : MonoBehaviour 
{
	public float speed;
	public GameObject explosionPrefab;
	private Vector3 destin;
	
	
	void Awake()
	{
		SetDestin();
		LevelManager.Instance.DecreaseRockets();
	}
	
	void Update()
	{ 
		Move();
		
		if (IsOnPosition())
		{
	        Instantiate(explosionPrefab, transform.position, transform.rotation);	
			Destroy(this.gameObject);
		}

	}
	
	void Move()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, destin, step);
	}
	
	//mark target position
	private void SetDestin()
	{

		destin = Utility.GetMouseWorldPosition();
		
	}	
	
	private bool IsOnPosition()
	{
		
		if (transform.position == destin)
		{
			return true;
		}
//        
		return false;
	}
}

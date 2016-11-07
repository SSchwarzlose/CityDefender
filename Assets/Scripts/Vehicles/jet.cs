using UnityEngine;
using System.Collections;

public class jet : MonoBehaviour {
	public float power = 300;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * power;
	}
}

using UnityEngine;
using System.Collections;

public class animationChopper : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Animation>().Play("Take 001");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

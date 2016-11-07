using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour 
{
	
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<AudioSource>().ignoreListenerPause = true;
		if (GameManager.PlayMusic)
		{
			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			if(GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Pause();
			}
		}
	}
	
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
}

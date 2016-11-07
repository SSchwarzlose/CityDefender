using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Invoke("LoadLevel", 1);
	}

    void LoadLevel()
    {
        Application.LoadLevelAsync(2);
    }
}

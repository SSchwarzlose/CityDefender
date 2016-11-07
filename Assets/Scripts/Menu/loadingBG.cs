using UnityEngine;
using System.Collections;

public class loadingBG : MonoBehaviour 
{


    public Texture2D mainTexture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
       
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mainTexture, ScaleMode.StretchToFill);
    }
}

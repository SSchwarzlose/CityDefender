using UnityEngine;
using System.Collections;

public class LevelLoading : MonoBehaviour 
{
    GameObject camera;
    public Texture2D ccc;
	// Use this for initialization
	void Start () 
    {
        //camera = GameObject.Find("Camera");
        float height = 2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * transform.position.z;
        float width = height * Screen.height / Screen.width;

        this.gameObject.transform.localScale.Scale(new Vector3 (width, 1, height));
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    
}

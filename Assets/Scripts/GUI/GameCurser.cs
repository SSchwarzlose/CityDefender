using UnityEngine;
using System.Collections;

public class GameCurser : MonoBehaviour 
{
    public Texture2D mouseCurser;
    public Texture2D crossFade;


	// Use this for initialization
	void Start () 
    {
        Cursor.visible = false;
	}
	
	void OnGUI()
    {
        GUI.depth = -1;
        if (GameManager.IsGamePaused || LevelManager.Instance.IsNextLevel)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 30, 30), mouseCurser);
        }
        else { GUI.DrawTexture(new Rect(Input.mousePosition.x - 30, Screen.height - Input.mousePosition.y - 30, 60, 60), crossFade); }
    }
}

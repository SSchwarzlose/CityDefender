using UnityEngine;
using System.Collections;

public class MouseCurser : MonoBehaviour {

    public Texture2D mouseCurser;

    void Start()
    {
        Cursor.visible = false;
    }

    void OnGUI()
    {
        GUI.depth = -1;
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 30, 30), mouseCurser);
    }
}

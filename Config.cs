using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Makes cursor visible
    public void ShowCursor()
    {
        Cursor.visible = true;
    }

    //Makes cursor invisible (while drunk?)
    public void HideCursor()
    {
        Cursor.visible = false;
    }
}

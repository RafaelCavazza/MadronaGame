﻿using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

    private bool starGame = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var click = Input.GetMouseButton(1);

        if(click && !starGame)
        {
            starGame = true;
            Application.LoadLevel("Main");
        }
	}
}
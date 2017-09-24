using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        base.Update();
	}

    void GetInput()
    {
        horDirection = Input.GetAxis("Horizontal");
    }
}

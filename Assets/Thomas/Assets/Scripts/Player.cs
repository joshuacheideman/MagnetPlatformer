using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	GameObject MagnetGuy;
	SpriteRenderer spriterender; //Get MagnetGuys Sprite Renderer
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		MagnetGuy = GameObject.Find ("MagnetGuy");
		spriterender = MagnetGuy.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        base.Update();
	}

    void GetInput()
    {
        horDirection = Input.GetAxis("Horizontal");
		if (horDirection > 0)
			spriterender.flipX = false;
		else if(horDirection < 0)
			spriterender.flipX = true;
    }
}

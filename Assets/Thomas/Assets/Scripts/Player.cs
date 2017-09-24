using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	GameObject MagnetGuy;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        Move();
	}

    void Move()
    {
        horDirection = Input.GetAxis("Horizontal");
		if (horDirection > 0)
			render.flipX = false;
		else if(horDirection < 0)
			render.flipX = true;
		rb.velocity = new Vector2(horDirection * speed * Time.deltaTime, rb.velocity.y);

		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(Vector2.up * jumpForce);
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	// Use this for initialization
	bool isGrounded=false;
	bool isCrouching = false;
	private const int Idle = 0;
	private const int JumpAnim =1;
	private const int Crouch = 2;
	private const int Moving =4;
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
		if ((horDirection > 0 || horDirection < 0)&&isGrounded)
			anim.SetInteger("state", Moving);
		if (horDirection == 0 && isGrounded&& !isCrouching)
			anim.SetInteger("state", Idle);
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			isCrouching = true;
			anim.SetInteger ("state", Crouch);
			horDirection = 0;
		} else 
			isCrouching = false;
		rb.velocity = new Vector2(horDirection * speed * Time.deltaTime, rb.velocity.y);
		if (isGrounded) {
			if (Input.GetButtonDown("Jump"))
			{
				rb.AddForce(Vector2.up * jumpForce);
				isGrounded = false;
				anim.SetInteger("state", JumpAnim);
			}
		}
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Floor")
			isGrounded = true;
	}
}

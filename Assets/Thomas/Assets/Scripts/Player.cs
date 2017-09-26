using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	// Use this for initialization
	bool isGrounded=false;
	bool isCrouching = false;
	bool isJumping =false;
	bool inMagnetZone=false;//checks to see if above Metal blocks
	bool JumpAtMaxHeight =false;
	bool isHovering=false;
	private const int Idle = 0;
	private const int JumpAnim =1;
	private const int Crouch = 2;
	private const int Moving =4;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected override void Update () {
		Move ();
		JumpAtMaxHeight=AtMaxHeight();
		if (JumpAtMaxHeight)//check if past maxheight of jump to hover
			Hover ();
		else
			isHovering = false;
	}
    void Move()
    {
        horDirection = Input.GetAxis("Horizontal");
		Flip ();
		MovementAnimations();
		rb.velocity = new Vector2(horDirection * speed * Time.deltaTime, rb.velocity.y);
		Jump ();
    }
	void Flip()
	{
		if (horDirection > 0)
			render.flipX = false;
		else if(horDirection < 0)
			render.flipX = true;	
	}
	void Jump()
	{
		if (isGrounded) {
			isJumping = Input.GetButtonDown("Jump");
			if (isJumping)
			{
				rb.AddForce(Vector2.up * jumpForce);
				isGrounded = false;
				anim.SetInteger("state", JumpAnim);
			}
		}
	}
	void MovementAnimations()
	{
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
	}
		void Hover()
	{
		isHovering = Input.GetButton ("Jump");
		if (isHovering&&inMagnetZone) {
			rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
		}
	}
	bool AtMaxHeight()
	{
		if (!isGrounded && rb.velocity.y < 0)
			return true;
		else
			return false;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
			isGrounded = true;
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "MagnetZone")
			inMagnetZone = true;
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "MagnetZone")
			inMagnetZone = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character {
	// Use this for initialization
	bool isGrounded=false;
	bool isCrouching = false;
	bool isJumping =false;
	bool inMagnetZone=false;//checks to see if above Metal blocks
	bool JumpAtMaxHeight =false;
	bool isHovering=false;
	bool PullRed = true;//toggle for which boxes you pull or repel
	private const int Idle = 0;
	private const int JumpAnim =1;
	private const int Crouch = 2;
	private const int Moving =4;
	RaycastHit2D[] AllBoxes = new RaycastHit2D[5];//amount of total red and blue boxes
	BoxBehavior MoveBox;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected override void Update () {
		Move ();
		ToggleMagnet ();
		//GetColliders ();
		JumpAtMaxHeight=AtMaxHeight();
		if (JumpAtMaxHeight)//check if past maxheight of jump to hover
			Hover ();
		else
			isHovering = false;
		GetColliders();
		MagnetPowers ();
		DeleteColliders ();
	}
    void Move()
    {
        horDirection = Input.GetAxis("Horizontal");
		Flip ();
		MovementAnimations();
		rb.velocity = new Vector2(horDirection * speed * Time.deltaTime, rb.velocity.y);
		Jump ();
    }
	void ToggleMagnet()
	{
		if (Input.GetButtonUp ("MagnetPull"))
			PullRed = !PullRed;
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
	void GetColliders()
	{
		Physics2D.CircleCastNonAlloc (transform.position, 14.0f, Vector2.right, AllBoxes, 1.0f, 1<<LayerMask.NameToLayer("MetalBlocks"));//do this to get all boxes that are in the circle
	}
	void DeleteColliders()//helper function to delete all colliders not with circlecast this frame.
	{
		Array.Clear (AllBoxes, 0, AllBoxes.Length);
	}
	bool AtMaxHeight()
	{
		if (!isGrounded && rb.velocity.y <= 0)
			return true;
		else
			return false;
	}
	void MagnetPowers()
	{
		foreach (RaycastHit2D hit in AllBoxes) {
			if (hit.collider == null)
				break;
			MoveBox = hit.collider.transform.gameObject.GetComponent<BoxBehavior> ();
			if (!Input.GetButton ("MagnetPull")) {
				Rigidbody2D BoxBody;
				BoxBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
				BoxBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
				BoxBody.velocity = new Vector2 (0.0f, 0.0f);
			}
			hit.collider.gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			if (hit.collider.gameObject.GetComponentInParent<SpriteRenderer> ().color == Color.red) {
				if(PullRed&&Input.GetButton("MagnetPull"))//this is the left ctrl key.
				{
					MoveBox.BoxPull (hit.collider.gameObject.transform);
					Debug.Log("Pull red");
				}
				else if(!PullRed&&Input.GetButton("MagnetPull"))
				{
					Debug.Log("Push Red");
					MoveBox.BoxPush (hit.collider.gameObject.transform);
				}
			}
			if (hit.collider.gameObject.GetComponentInParent<SpriteRenderer> ().color == Color.blue) {
				
				if (!PullRed && Input.GetButton ("MagnetPull"))
					MoveBox.BoxPull (hit.collider.gameObject.transform);
				if (PullRed && Input.GetButton ("MagnetPull"))
					MoveBox.BoxPush (hit.collider.gameObject.transform);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
			isGrounded = true;
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "MagnetHoverZone")
			inMagnetZone = true;
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "MagnetHoverZone")
			inMagnetZone = false;
	}
}

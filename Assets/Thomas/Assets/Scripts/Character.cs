using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float jumpForce;

    protected Rigidbody2D rb;
    protected float horDirection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        Move();
	}

    private void Move()
    {
        rb.velocity = new Vector2(horDirection * speed * Time.deltaTime, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}

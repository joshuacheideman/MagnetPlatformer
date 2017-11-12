using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : Character {
    private float animSpeedFactor = .5f;

    [SerializeField]
    protected Vector3 startingVelocity;

    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = startingVelocity;
    }
        // Update is called once per frame
        void Update () {
        anim.speed = Mathf.Abs(rb.velocity.x * animSpeedFactor);
        if (rb.velocity.x < 0) {
            anim.SetBool("isMovingRight", false);
        }
        else {
            anim.SetBool("isMovingRight", true);
        }
    }

    public void setVelocity(Vector2 vec) {
        rb.velocity = vec;
    }

    public void addForce(Vector2 vec) {
        rb.AddForce(vec);
    }
}

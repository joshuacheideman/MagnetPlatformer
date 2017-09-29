using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float cooldown; // Cooldown of action in seconds

    protected float curCooldown = 0;
    protected bool isGrounded = false;
    protected bool isAttacking = false;
    protected GameObject target;
    // Use this for initialization
    protected void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
	}

    virtual protected void FixedUpdate() {
        if (!target) {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected Vector3 getVectorToTarget() {
        if (target) {
            Vector3 curPos = this.transform.position;
            Vector3 targetPos = target.transform.position;
            Vector3 toPlayer = targetPos - curPos;
            float xdirection = toPlayer.x;
            return toPlayer;
        }
        else {
            return Vector3.zero;
        }
    }

    /**
    * Flips sprite if if is falling down
     */
    protected void flipIfFalling() {
        if (rb.velocity.y <= 0) {
            render.flipY = true;
        }
        if (isGrounded) {
            render.flipY = false;
            isAttacking = false;
        }
    }

    virtual protected void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Floor") {
            isGrounded = true;
        }
    }


}

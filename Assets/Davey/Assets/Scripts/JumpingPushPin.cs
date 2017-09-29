using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPushPin : Enemy {

    private float originalAnimSpeed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        originalAnimSpeed = anim.speed;
    }
    // Update is called once per frame
    override protected void FixedUpdate() {
        base.FixedUpdate();
        flipIfFalling();
        if (curCooldown <= 0 && isGrounded) {
            anim.speed = 0;
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            rb.AddForce(speed * Vector3.right);
            isAttacking = true;
            curCooldown = cooldown;
        }
        else if (isGrounded) {
            curCooldown -= Time.deltaTime;
            anim.speed = originalAnimSpeed;
        }

    }

    override protected void OnCollisionEnter2D(Collision2D col) {
        base.OnCollisionEnter2D(col);
        if (col.gameObject.tag == "Player") {
            Debug.Log("Ow, player took " + damage + " damage");
            //col.gameObject.SendMessage("takeDamage", damage);
        }
    }
}

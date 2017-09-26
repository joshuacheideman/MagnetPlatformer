using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPushPin : Enemy {

    
    // Update is called once per frame
    override protected void FixedUpdate() {
        base.FixedUpdate();
        flipIfFalling();
        if (!isAttacking && curCooldown <= 0) {
            if (target) {
                Vector3 toTarget = getVectorToTarget();
                if (Mathf.Abs(toTarget.x) < detectionRange && isGrounded) {
                    rb.AddForce(Vector2.up * jumpForce);
                    isGrounded = false;
                    if (toTarget.x > 0) {
                        rb.AddForce(speed * Vector3.right);
                    }
                    else {
                        rb.AddForce(speed * -Vector3.right);
                    }
                    
                    isAttacking = true;
                    curCooldown = cooldown;
                }
            }
        }
        //else if (isAttacking && getVectorToTarget().x < 1 && rb.velocity.y > 0) {
        //    rb.velocity = new Vector2(0, 0);
        //}
        else if (!isAttacking && curCooldown > 0) {
            curCooldown -= Time.deltaTime;
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

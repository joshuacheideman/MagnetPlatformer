using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPushPin : Enemy {

    [SerializeField]
    protected float cooldown; // Cooldown of jump in seconds

    private float curCooldown = 0;
    // Update is called once per frame
    override protected void FixedUpdate() {
        base.FixedUpdate();
        if (rb.velocity.y <= 0) {
            render.flipY = true;
        }
        if (isGrounded) {
            render.flipY = false;
            isAttacking = false;
        }

        if (!isAttacking && curCooldown <= 0) {
            if (target) {
                Debug.Log("Finding target");
                Vector3 toTarget = getVectorToTarget();
                Debug.Log("Grounded:" + isGrounded);
                Debug.Log(toTarget.x);
                if (Mathf.Abs(toTarget.x) < detectionRange && isGrounded) {
                    Debug.Log("Attack");
                    rb.AddForce(Vector2.up * jumpForce);
                    isGrounded = false;
                    rb.AddForce(speed * Vector3.right);
                    isAttacking = true;
                    curCooldown = cooldown;
                }
                else {
                    //idle();
                }
            }
        }
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

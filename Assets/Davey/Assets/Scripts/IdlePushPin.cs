using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePushPin : Enemy {
	
	// Update is called once per frame
	override protected void FixedUpdate () {
        base.FixedUpdate();
        idle();
    }

    protected void idle() {
        if (isGrounded) {
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            //anim.SetInteger("state", JumpAnim);
        }
    }

    virtual protected void OnCollisionEnter2D(Collision2D col) {
        base.OnCollisionEnter2D(col);
        if (col.gameObject.tag == "Player") {
            Debug.Log("Ow, player took " + damage + " damage");
            //col.gameObject.SendMessage("takeDamage", damage);
        }
    }
}

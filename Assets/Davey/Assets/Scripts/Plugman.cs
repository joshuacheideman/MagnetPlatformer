using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plugman : Enemy {
    [SerializeField]
    protected Transform orbSpawn;
    [SerializeField]
    protected GameObject projectile;

    // Update is called once per frame

    // Use this for initialization
    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
       
    }

    bool AnimatorIsPlaying() {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName) {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    override protected void  FixedUpdate () {
        if (AnimatorIsPlaying("AttackPlugman") && !isAttacking) {
            isAttacking = true;
            Instantiate(projectile, orbSpawn.position, orbSpawn.rotation);
        }
        if (!AnimatorIsPlaying("AttackPlugman")) {
            isAttacking = false;
        }
        if (target) {
            Vector3 toTarget = getVectorToTarget();
            if (toTarget.x < detectionRange && !isAttacking) {
                anim.SetTrigger("isAttacking");
            }
        }
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float speed;
	[SerializeField]
	protected float duration;
	[SerializeField]
	protected bool isDestroyedOnWall;

    protected Rigidbody2D rb;
    protected GameObject target;


	abstract protected void OnCollisionEnter2D (Collision2D col);

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
}

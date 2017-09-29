using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float speed;

    protected Rigidbody2D rb;
    protected GameObject target;

    // Use this for initialization
    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        rb.AddForce(getVectorToTarget().normalized * speed);
    }

    virtual protected void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Floor") {
            Destroy(this.gameObject);
        }
    }

    protected void Update() {
        this.transform.Rotate(Vector3.forward * Time.deltaTime * 5, Space.World);
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
}

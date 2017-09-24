using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float jumpForce;
	[SerializeField]
	protected SpriteRenderer render;
    protected Rigidbody2D rb;
    protected float horDirection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	}

    protected virtual void Move()
    {
    }
}

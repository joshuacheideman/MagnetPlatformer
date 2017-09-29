using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour {
    [SerializeField]
    protected int maxHealth;

    protected int currentHealth;

	// Use this for initialization
	protected virtual void Start () {
        currentHealth = maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

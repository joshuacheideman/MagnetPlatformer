using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
    private Image healthBar;

    [SerializeField]
    private AudioSource hurtSound;

    [SerializeField]
    private AudioSource loseSound;

    [SerializeField]
    private Player playerScript;

    private Rigidbody2D rb;

    [SerializeField]
    private float timeToTakeDamage;

    private float damageTimer;

    [SerializeField]
    private float flashDuration;

    private float flashTimer;

    [SerializeField]
    private SpriteRenderer playerSprite;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        try
        {
            healthBar = GameObject.FindGameObjectWithTag("UI").transform.Find("HealthBar").GetComponent<Image>();
        }
        catch
        {
            Debug.Log("UI or HealthBar not found");
        }

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // DEBUG TOOL
		if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("1 Points of damage applied");
            TakeDamage(1);
        }

        if (damageTimer > 0)
            damageTimer -= Time.deltaTime;

        if (!playerSprite)
            return;

        if (flashTimer > 0)
        {
            playerSprite.color = Color.red;
            flashTimer -= Time.deltaTime;
        }
        else
        {
            playerSprite.color = Color.white;
        }
	}

    protected override void TakeDamage(int damage)
    {
        // Time it takes to take damage again
        if (damageTimer > 0)
            return;

        base.TakeDamage(damage);
        hurtSound.Play();
        UpdateHealthBar();
        damageTimer = timeToTakeDamage;
        flashTimer = flashDuration;
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float) currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            loseSound.Play();
            Destroy(playerScript);
            Destroy(rb);
            Destroy(playerSprite);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Kill"))
            TakeDamage(100);
    }
}

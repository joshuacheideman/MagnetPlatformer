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

    [SerializeField]
    private SpriteRenderer playerSprite;

    private GameManager gm;

    [SerializeField]
    private float deathDuration;

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
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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

        if (currentHealth > 0)
            StartCoroutine(Flash(flashDuration));
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

            StartCoroutine(Death(deathDuration));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Kill"))
        {
            TakeDamage(100);
        }
    }

    IEnumerator Flash(float waitTime)
    {

        Color originalColor = playerSprite.color;
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(waitTime);
        playerSprite.color = originalColor;
    }

    IEnumerator Death(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gm.RestartScene();
    }
}

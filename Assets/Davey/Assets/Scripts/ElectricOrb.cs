using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrb :  Projectile {

	override protected void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Floor" && isDestroyedOnWall) {
			Destroy(this.gameObject);
		}
		if (col.gameObject.tag == "Player") {
			onHitPlayer();
			Destroy(this.gameObject);
		}
	}

	protected void Start() {
		startHelper();
	}

	protected void startHelper() {
		StartCoroutine (expire ());
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player");
		Vector3 forceVector = getVectorToTarget ().normalized * speed;
		rb.AddForce(forceVector);
	}

	virtual protected void onHitPlayer() {
		Debug.Log ("Player was hit");
	}


	protected void Update() {
		this.transform.Rotate(Vector3.forward * Time.deltaTime * 5, Space.World);
	}
		

	IEnumerator expire() {
		blink ();
		yield return new WaitForSeconds(duration - 1);
		StartCoroutine (blink ());
		yield return new WaitForSeconds(1);
		Debug.Log ("Expiring");
		Destroy (this.gameObject);
	}

	 


	IEnumerator blink() {
		SpriteRenderer spr = GetComponent<SpriteRenderer>();
		while (true) {
			spr.enabled = false;
			yield return new WaitForSeconds (.1f);
			spr.enabled = true;
			yield return new WaitForSeconds (.1f);
		}

	}

}

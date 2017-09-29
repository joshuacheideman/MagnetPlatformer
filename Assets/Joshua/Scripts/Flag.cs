using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Flag : MonoBehaviour {
	Animator anim;
	GameManager manager;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		manager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			anim.SetBool ("RaiseFlag", true);
			manager.SwitchScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}

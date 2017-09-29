using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Flag : MonoBehaviour {
	Animator anim;
	GameManager manager;
    [SerializeField]
    private float flagRaiseTime;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		manager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			anim.SetBool ("FlagRaised", true);
            StartCoroutine(WaitFlag());
		}
	}

    IEnumerator WaitFlag()
    {
        yield return new WaitForSeconds(flagRaiseTime);
        manager.SwitchScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

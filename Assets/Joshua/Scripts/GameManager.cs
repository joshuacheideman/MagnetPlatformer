using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour {
	public static GameManager instance = null;


	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		ReturnToMenu ();
	}

	public void ReturnToMenu()
	{
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    SceneManager.LoadScene(0);//0 is Main Menu Scene
        //    Destroy(gameObject);
        //}
	}
}

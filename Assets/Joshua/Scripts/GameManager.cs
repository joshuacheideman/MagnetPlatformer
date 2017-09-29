using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	bool isPaused=false;
	int SceneNumer;
    [SerializeField]
    private GameObject[] initializables;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Object.DontDestroyOnLoad(gameObject);

        // For Debugging since other levels don't have a gamemanager
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            InitializeLevel();
        }
    }
	
	// Update is called once per frame
	void Update () {
		ReturnToMenu ();
		Pause ();
        DebugScene();
	}
	public void ReturnToMenu()
	{
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);//0 is Main Menu Scene
            Destroy(gameObject);
        }
	}
	void Pause()//TODO make an interactive pause menu
	{
		if (isPaused&&Input.GetButtonDown("Menu")) 
		{
			isPaused = false;
			Time.timeScale = 1.0f;//normal speed
			Debug.Log ("Resuming");
			return;
		}
		if (!isPaused&&Input.GetButtonDown ("Menu"))//hit Tab for Menu 
		{
			isPaused = true;
			Time.timeScale = 0;//nothing is moving
			Debug.Log ("Paused");
			return;
		}
	}
	public void SwitchScene(int SceneNo)//use this to switch scenes
	{
		SceneManager.LoadScene (SceneNo);
	}

    private void InitializeLevel()
    {
        foreach (GameObject GO in initializables)
        {
            Instantiate(GO, Vector2.zero, Quaternion.identity);
        }
    }
	private void DestroyLevel()//delete current gameObjects
	{
		
	}

    /// <summary>
    /// Debug tool for checking level initialization when switching scenes
    /// </summary>
    private void DebugScene()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchScene(SceneManager.GetActiveScene().buildIndex + 1);
			if(SceneManager.GetActiveScene().buildIndex + 1<=SceneManager.sceneCount-1)
            	InitializeLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    public void RestartScene()
    {
        SwitchScene(SceneManager.GetActiveScene().buildIndex);
    }
}

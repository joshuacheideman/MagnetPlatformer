using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	bool isPaused=false;
	int SceneNumer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		ReturnToMenu ();
		Pause ();
	}
	public void ReturnToMenu()
	{
		if(Input.GetButtonDown("Cancel"))
			SceneManager.LoadScene (0);//0 is Main Menu Scene
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
}

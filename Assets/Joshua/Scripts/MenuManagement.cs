using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManagement : MonoBehaviour {

	//Start Game
	public void StartGame()
	{
		SceneManager.LoadScene (1);
	}
	//Quit out of Application
	public void QuitGame()
	{
		Application.Quit ();
	}
}

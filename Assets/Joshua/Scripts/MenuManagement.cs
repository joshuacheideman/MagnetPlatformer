using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManagement : MonoBehaviour {
    private AudioSource click;

    private void Start()
    {
        click = GetComponent<AudioSource>();
    }

    //Start Game
    public void StartGame()
	{
        Click();
        SceneManager.LoadScene (1);
	}
	//Quit out of Application
	public void QuitGame()
	{
        Click();
        Application.Quit ();
	}

    private void Click()
    {
        click.Play();
    }
}

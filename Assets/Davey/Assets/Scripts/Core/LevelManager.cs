using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Private reference to LevelManager singleton
    /// </summary>
	private static LevelManager _levelManager;
  
    /// <summary>
    /// GameObject that acts as fade effect
    /// </summary>
    public GameObject _fade;

    public static LevelManager instance
    {
        get
        {
            return _levelManager;
        }
    }

    private void Awake()
    {
        if (_levelManager != null && _levelManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {

            _levelManager = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn(scene));
    }

    public void NextLevel()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLevel(int newBuildIndex)
    {
        //_eventManager.EndCutScene(false);
        StartCoroutine(FadeOut(newBuildIndex));
    }

    IEnumerator FadeOut(int desiredBuildIndex)
    {
        Scene scene = SceneManager.GetActiveScene();
        float alpha = 0f;
        if (scene.name != "MainMenu")
        {
            while (alpha <= 1)
            {
                Image fadeImage = _fade.GetComponent<Image>();
                Color origColor = fadeImage.color;
                fadeImage.color = new Color(0, 0, 0, alpha);
                alpha += .05f;
                yield return new WaitForSeconds(.01f);
            }
        }
        if (SceneManager.sceneCountInBuildSettings <= desiredBuildIndex)
        {
            ReturnToTitle();
        }
        else
        {
            SceneManager.LoadScene(desiredBuildIndex);
        }
    }

    IEnumerator FadeIn(Scene scene)
    {
        SoundManager.instance.OnSceneLoad(scene.name);
        float alpha = 1f;
        if (scene.name != "MainMenu")
        {
            while (alpha >= .01)
            {
                Image fadeImage = _fade.GetComponent<Image>();
                Color origColor = fadeImage.color;
                fadeImage.color = new Color(0, 0, 0, alpha);
                alpha -= .05f;
                yield return new WaitForSeconds(.05f);
            }

        }
        //eventManager.onSceneLoad(scene.name);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }

    // Use this for initialization
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    /// <summary>
    /// Resets level
    /// </summary>
    public void ResetLevel()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

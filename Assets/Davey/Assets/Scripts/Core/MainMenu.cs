using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int firstLevelIndex;
    [SerializeField]
    private GameObject _titleScreenComponents;
    [SerializeField]
    private GameObject _sceneSelectComponents;
    [SerializeField]
    private GameObject _optionsComponents;

    private GameObject _curSelectedComponents;

    // Use this for initialization
    void Start()
    {
        _curSelectedComponents = _titleScreenComponents;
        SwapMenuScreens(_titleScreenComponents);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void PlayMenuNoise()
    {
        SoundManager.instance.PlayMenuNoise();
    }

    public void ChangeLevel(int newBuildIndex)
    {
        LevelManager.instance.ChangeLevel(newBuildIndex);
    }

    public void StartGame()
    {
        LevelManager.instance.ChangeLevel(firstLevelIndex);
    }

    /// <summary>
    /// Sets currently select menu to scene select
    /// </summary>
    public void ToSceneSelect()
    {
        SwapMenuScreens(_sceneSelectComponents);
    }

    /// <summary>
    /// Sets currently select menu to title screen
    /// </summary>
    public void ToTitleScreen()
    {
        SwapMenuScreens(_titleScreenComponents);
    }

    /// <summary>
    /// Sets currently select menu to options
    /// </summary>
    public void ToOptions()
    {
        SwapMenuScreens(_optionsComponents);
    }

    /// <summary>
    /// Sets menu to the new screen
    /// Disables current components and sets and enables new component
    /// </summary>
    /// <param name="newScreen">Gameobjects to show</param>
    private void SwapMenuScreens(GameObject newScreen)
    {
        _curSelectedComponents.SetActive(false);
        _curSelectedComponents = newScreen;
        _curSelectedComponents.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_curSelectedComponents.transform.GetChild(0).gameObject);
    }

    public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}

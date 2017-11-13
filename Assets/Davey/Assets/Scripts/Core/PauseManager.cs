using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    [SerializeField]
    private GameObject _pauseMenuComponents;
    [SerializeField]
    private GameObject _optionsMenuComponents;

    private GameManager _gameManager;

    bool isPaused = false;

    public enum PauseState
    {
        /// <summary>
        /// Not paused
        /// </summary>
        NONE,
        /// <summary>
        /// Main pause screen
        /// </summary>
        PAUSE,
        /// <summary>
        /// Paused options screen
        /// </summary>
        OPTIONS
    }

    /// <summary>
    /// Currently active component
    /// Starts at none
    /// </summary>
    private PauseState _curState;

    private Dictionary<PauseState, GameObject> stateMapping;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.instance;
        stateMapping = new Dictionary<PauseState, GameObject>()
        {
            {PauseState.NONE, null},
            {PauseState.PAUSE, _pauseMenuComponents},
            {PauseState.OPTIONS, _optionsMenuComponents}
        };
        _curState = PauseState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
        CheckPause();
	}

    void CheckPause()//TODO make an interactive pause menu
    {
        if (isPaused && Input.GetButtonDown("Menu"))
        {
            Unpause();
        }
        if (!isPaused && Input.GetButtonDown("Menu"))//hit Tab for Menu 
        {
            Pause();
        }
    }

    private void Unpause()
    {
        isPaused = false;
        ChangeState(PauseState.NONE);
        Time.timeScale = 1.0f;//normal speed
        //TODO: enable player input
    }

    public void Pause()
    {
        //TODO: Stop player input
        isPaused = true;
        ChangeState(PauseState.PAUSE);
        Time.timeScale = 0;//nothing is moving
    }

    /// <summary>
    /// Sets pause state to pause
    /// </summary>
    public void ToPause()
    {
        ChangeState(PauseState.PAUSE);
    }

    /// <summary>
    /// Sets pause state to none
    /// </summary>
    public void ToUnpause()
    {
        ChangeState(PauseState.NONE);
    }

    /// <summary>
    /// Sets pause state to options
    /// </summary>
    public void ToOptions()
    {
        ChangeState(PauseState.OPTIONS);
    }

    /// <summary>
    /// Changes pause menu state
    /// </summary>
    /// <param name="newState">New PauseState</param>
    private void ChangeState(PauseState newState)
    {
        if (_curState != PauseState.NONE)
        {
            stateMapping[_curState].SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        _curState = newState;
        if (newState != PauseState.NONE)
        {
            Unpause();
            stateMapping[newState].SetActive(true);
            EventSystem.current.SetSelectedGameObject(stateMapping[newState].transform.GetChild(0).gameObject);
        }
    }
}

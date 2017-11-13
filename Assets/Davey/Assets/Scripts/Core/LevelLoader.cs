using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField]
    private GameObject[] initializables;
    // Use this for initialization
    void Start () {
        foreach (GameObject GO in initializables)
        {
            Instantiate(GO, Vector2.zero, Quaternion.identity);
        }
    }

    private void DestroyLevel()//delete current gameObjects
    {

    }
}

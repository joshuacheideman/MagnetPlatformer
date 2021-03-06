﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 cameraPosition;
        cameraPosition = player.transform.position;
        cameraPosition.z = -1;
        transform.position = cameraPosition;
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        try
        {
            transform.position = player.transform.position + offset;
        }
        catch
        {
            Debug.Log("No Player");
        }
	}
}

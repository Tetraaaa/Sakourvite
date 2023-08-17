using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    GameObject player;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null) transform.position = player.transform.position + offset;
    }
}
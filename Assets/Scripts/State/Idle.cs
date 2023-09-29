using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private PlayerController _controller;
    
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not on ground we fall
        if (!_controller.grounded)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            this.enabled = false;
            GetComponent<Running>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _controller.grounded)
        {
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{

    // Update is called once per frame
    void Update()
    {
        // If not on ground we fall
        if (!_controller.grounded)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Mathf.Abs(move.ReadValue<Vector2>().x) > 0.01f)
        {
            this.enabled = false;
            GetComponent<Running>().enabled = true;
        }
        else if (jump.WasPressedThisFrame() && _controller.grounded)
        {
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
    }
}

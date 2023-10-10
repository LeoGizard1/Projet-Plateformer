using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Running : PlayerState
{
    [SerializeField] private float speed;

    void Update()
    {
        if (!_controller.grounded) {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        } else if (Mathf.Abs(move.ReadValue<Vector2>().x) < 0.01f) {
            this.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            GetComponent<Idle>().enabled = true;
        }  else if (jump.WasPressedThisFrame() && _controller.grounded)
        {
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
        else
        {
            _rigidbody.velocity = new Vector2(move.ReadValue<Vector2>().x, 0) * speed;
        }
    }
}

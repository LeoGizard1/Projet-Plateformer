using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_controller.grounded) {
            this.enabled = false;
            //GetComponent<Falling>().enabled = true;
        } else if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f) {
            this.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            GetComponent<Idle>().enabled = true;
            return;
        }  else if (Input.GetKeyDown(KeyCode.Space) && _controller.grounded)
        {
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
        else
        {
            _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"),0) * speed;
        }
    }
}

using System;

using UnityEngine;

public class Falling : MonoBehaviour
{
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
        if (_controller.IsOnGround)
        {
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }

        var velocity = _rigidbody.velocity;
        velocity.y = -5f;
        _rigidbody.velocity = velocity;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }


}

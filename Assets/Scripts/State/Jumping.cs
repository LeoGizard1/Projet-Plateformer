using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float deceleration;

    private void OnEnable()
    {
        _rigidbody.velocity = new Vector2(0, jumpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!jump.IsPressed() || _rigidbody.velocity.y < 0)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        }

        _rigidbody.velocity = 
            new Vector2(move.ReadValue<Vector2>().x * horizontalSpeed, _rigidbody.velocity.y - deceleration * Time.deltaTime);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalWallJumpSpeed;
    [SerializeField] private float wallJumpDeceleration;

    private int wallDirection = 0;
    private void OnEnable()
    {
        SetWallDirection();
        _rigidbody.velocity = new Vector2(jumpSpeed * wallDirection, jumpSpeed);
    }

    void Update()
    {
        if (!jump.IsPressed() || _rigidbody.velocity.y < 0)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        // Setting a goal vector for the velocity so that the player can move and jump away from the wall
        Vector2 goalVelocity = new Vector2(move.ReadValue<Vector2>().x * horizontalWallJumpSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity, goalVelocity, 0.2f);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - wallJumpDeceleration * Time.deltaTime);
    }

    public void SetWallDirection()
    {
        switch(_controller.isOnWall)
        {
            case IsOnWall.None:
                wallDirection = 0;
                break;
            case IsOnWall.Left:
                wallDirection = 1;
                break;
            case IsOnWall.Right:
                wallDirection = -1;
                break;
        }
    }
}

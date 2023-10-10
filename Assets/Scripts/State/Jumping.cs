using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float deceleration;

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
        // magic
        Vector2 goal = new Vector2(move.ReadValue<Vector2>().x * horizontalSpeed,_rigidbody.velocity.y);
        _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity, goal, 0.2f);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - deceleration * Time.deltaTime);
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

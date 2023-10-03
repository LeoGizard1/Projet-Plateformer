using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum CollisionDirection
    {
        Left,
        Right,
        Up,
        Down,
    }

    private Vector2 velocity;
    private Vector2 acceleration;
    
    
    public bool grounded { get; private set; } = false;
    private GameObject ground;

    private Rigidbody2D _rigidbody2D;

    private Collider2D _collider2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {
        OffsetCollider();
    }

    //offset collider to prevent clipping in the wall/ground.
    private void OffsetCollider()
    {
        _collider2D.offset = _rigidbody2D.velocity * Time.fixedDeltaTime;
    }

    private void SnapToPlatform(Transform platformTransform, CollisionDirection direction)
    {
        switch (direction)
        {
            case CollisionDirection.Down:
                _rigidbody2D.position = 
                    new Vector2(_rigidbody2D.position.x,
                                platformTransform.position.y + (0.5f + 1f * platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Up:
                _rigidbody2D.position = 
                    new Vector2(_rigidbody2D.position.x, 
                                platformTransform.position.y - (0.5f + 1f * platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Right:
                _rigidbody2D.position =
                    new Vector2(platformTransform.position.x - (0.5f + 1f * platformTransform.lossyScale.x / 2f),
                                _rigidbody2D.position.y);
                break;
            case CollisionDirection.Left:
                _rigidbody2D.position = 
                    new Vector2(platformTransform.position.x + (0.5f + 1f * platformTransform.lossyScale.x / 2f),
                                _rigidbody2D.position.y);
                break;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision(other);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collision2D other)
    {
        var position = transform.position;
        var downCast = Physics2D
            .BoxCast(position + Vector3.down *0.6f  , new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0f).collider;
        var upCast = Physics2D
            .BoxCast((position + Vector3.up*0.6f), new Vector2(0.5f, 0.2f), 0f, Vector2.up, 0f).collider;
        var leftCast = Physics2D
            .BoxCast(position + Vector3.left*0.6f, new Vector2(0.2f, 0.5f), 0f, Vector2.left, 0f).collider;
        var rightCast = Physics2D
            .BoxCast(position + Vector3.right*0.6f, new Vector2(0.2f, 0.5f), 0f, Vector2.right, 0f).collider;
        if (other.collider == downCast)
        {
            ground = other.collider.gameObject;
            grounded = true;
            SnapToPlatform(other.transform, CollisionDirection.Down);
        }
        else if (other.collider == upCast)
        {
            SnapToPlatform(other.transform, CollisionDirection.Up);
        }
        else if (other.collider == leftCast)
        {
            SnapToPlatform(other.transform, CollisionDirection.Left);
        }
        else if (other.collider == rightCast)
        {
            SnapToPlatform(other.transform, CollisionDirection.Right);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == ground)
            grounded = false;
    }
    
}
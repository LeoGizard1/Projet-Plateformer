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
        _collider2D.offset = _rigidbody2D.velocity * Time.deltaTime;
    }
    
    private void SnapToPlatform(Transform groundTransform, CollisionDirection direction)
    {
        Debug.Log(String.Format("Snapping to {0} {1}",groundTransform,direction));
        switch (direction)
        {
            case CollisionDirection.Down:
                _rigidbody2D.position = new Vector2(_rigidbody2D.position.x,
                                                groundTransform.position.y + 1f);
                break;
            case CollisionDirection.Up:
                _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, groundTransform.position.y - 1f); 
                break;
            case CollisionDirection.Right:
                _rigidbody2D.position = new Vector2(groundTransform.position.x - 0.5f - 1f * groundTransform.lossyScale.x/2f, _rigidbody2D.position.y);
                break;

        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        var downCast = Physics2D.BoxCast(transform.position + Vector3.down, new Vector2(0.8f,0.2f),0f,Vector2.down,0f).collider;
        var upCast = Physics2D.BoxCast((transform.position + Vector3.up),new Vector2(0.8f,0.2f), 0f, Vector2.up,0f).collider;
        var leftCast = Physics2D.BoxCast(transform.position + Vector3.left, new Vector2(0.2f,0.8f), 0f, Vector2.left,0f).collider;
        var rightCast = Physics2D.BoxCast(transform.position + Vector3.right,new Vector2(0.2f,0.8f), 0f,Vector2.right,0f).collider;
        if (other.collider == downCast)
        {
            ground = other.collider.gameObject;
            grounded = true;
            SnapToPlatform(other.transform,CollisionDirection.Down);
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

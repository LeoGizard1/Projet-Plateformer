using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IsOnWall
{
    Left,
    Right,
    None
}

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

    public IsOnWall isOnWall { get; private set; } = IsOnWall.None;
    private GameObject wall;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private CameraManager _camManager;
    private Vector2 spawnPoint;
    private ParticleSystem[] smokeBombs;
    private ParticleSystem victoryParticles;
    private TrailRenderer _trail;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _camManager = GetComponent<CameraManager>();
        victoryParticles = GameObject.Find("VictoryParticleSystem").GetComponent<ParticleSystem>();
        _trail = GetComponentInChildren<TrailRenderer>();

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("SmokeBombs");
        smokeBombs = new ParticleSystem[bombs.Length];
        for (int i = 0; i < bombs.Length; i++)
        {
            smokeBombs[i] = bombs[i].GetComponent<ParticleSystem>();
        }

        spawnPoint = new Vector2(0, 0);
        GameObject spawn = GameObject.Find("SpawnPoint");
        if(spawn != null )
            spawnPoint = spawn.transform.position;
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
        Debug.Log(String.Format("Snapping to {0} {1}",transform.gameObject.name,direction));
        switch (direction)
        {
            case CollisionDirection.Down:
                _rigidbody2D.position = 
                    new Vector2(_rigidbody2D.position.x,
                                platformTransform.position.y + (transform.localScale.y/2.0f + platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Up:
                _rigidbody2D.position = 
                    new Vector2(_rigidbody2D.position.x, 
                                platformTransform.position.y - (transform.localScale.y/2.0f + platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Right:
                _rigidbody2D.position =
                    new Vector2(platformTransform.position.x - (transform.localScale.x/2.0f + platformTransform.lossyScale.x / 2f),
                                _rigidbody2D.position.y);
                break;
            case CollisionDirection.Left:
                _rigidbody2D.position = 
                    new Vector2(platformTransform.position.x + (transform.localScale.x/2.0f + platformTransform.lossyScale.x / 2f),
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
        var localScale = transform.localScale;
        var downCast = Physics2D
            .BoxCast(position + Vector3.down*(localScale.y/2.0f+0.1f)  , new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0f).collider;
        var upCast = Physics2D
            .BoxCast((position + Vector3.up*(localScale.y/2.0f+0.1f)), new Vector2(0.5f, 0.2f), 0f, Vector2.up, 0f).collider;
        var leftCast = Physics2D
            .BoxCast(position + Vector3.left*(localScale.x/2.0f+0.1f), new Vector2(0.2f, 0.5f), 0f, Vector2.left, 0f).collider;
        var rightCast = Physics2D
            .BoxCast(position + Vector3.right*(localScale.x/2.0f+0.1f), new Vector2(0.2f, 0.5f), 0f, Vector2.right, 0f).collider;
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
            wall = other.collider.gameObject;
            isOnWall = IsOnWall.Left;
            SnapToPlatform(other.transform, CollisionDirection.Left);
        }
        else if (other.collider == rightCast)
        {
            wall = other.collider.gameObject;
            isOnWall = IsOnWall.Right;
            SnapToPlatform(other.transform, CollisionDirection.Right);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == ground)
        {
            grounded = false;
        }
        if (other.gameObject == wall)
            isOnWall = IsOnWall.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lethal"))
        {
            smokeBombs[0].transform.position = transform.position;
            smokeBombs[1].transform.position = spawnPoint;
            smokeBombs[0].Play();
            smokeBombs[1].Play();
            _trail.emitting = false;
            _trail.Clear();
            transform.position = spawnPoint;
            _camManager.moveCamera(0, 0f);
            _trail.Clear();
            _trail.emitting = true;
        }
        else if (collision.CompareTag("Transition"))
        {
            int id = 0;
            if (collision.gameObject.name == "to1") id = 1;
            if (collision.gameObject.name == "to2") id = 2;
            _camManager.moveCamera(id, 0.5f);
        }
        else if (collision.CompareTag("Victory"))
        {
            victoryParticles.Play();
        }
    }
}

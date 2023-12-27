using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum IsOnWall
{
    Left,
    Right,
    None
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float gravity;
    public Vector2 direction = new Vector2(1, 1).normalized;
    public float power = 10;
    [SerializeField] float maxPower;
    [SerializeField] float minPower;
    
    public bool jumpPressed = false;

    private CameraManager camManager;
    private Collider2D collider2d;

    private Rigidbody2D rigidbody2d;
    private TrailRenderer trail;
    private GameObject ground;
    private ParticleSystem[] smokeBombs;
    private Vector2 spawnPoint;
    private ParticleSystem victoryParticles;
    private GameObject wall;
    

    public bool Grounded { get; private set; }

    public IsOnWall IsOnWall { get; private set; } = IsOnWall.None;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        camManager = GetComponent<CameraManager>();
        victoryParticles = GameObject.Find("VictoryParticleSystem").GetComponent<ParticleSystem>();
        trail = GetComponentInChildren<TrailRenderer>();

        var bombs = GameObject.FindGameObjectsWithTag("SmokeBombs");
        smokeBombs = new ParticleSystem[bombs.Length];
        for (var i = 0; i < bombs.Length; i++) smokeBombs[i] = bombs[i].GetComponent<ParticleSystem>();

        spawnPoint = new Vector2(0, 0);
        var spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
            spawnPoint = spawn.transform.position;
    }

    private void FixedUpdate()
    {
        OffsetCollider();
    }

    private void LateUpdate()
    {
        jumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == ground) Grounded = false;
        if (other.gameObject == wall)
            IsOnWall = IsOnWall.None;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        HandleCollision(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lethal"))
        {
            smokeBombs[0].transform.position = transform.position;
            smokeBombs[1].transform.position = spawnPoint;
            smokeBombs[0].Play();
            smokeBombs[1].Play();
            trail.emitting = false;
            trail.Clear();
            transform.position = spawnPoint;
            camManager.MoveCamera(0, 0f);
            trail.Clear();
            trail.emitting = true;
        }
        else if (collision.CompareTag("Transition"))
        {
            var id = 0;
            if (collision.gameObject.name == "to1") id = 1;
            if (collision.gameObject.name == "to2") id = 2;
            camManager.MoveCamera(id, 0.5f);
        }
        else if (collision.CompareTag("Victory"))
        {
            victoryParticles.Play();
        }
    }

    //offset collider to prevent clipping in the wall/ground.
    private void OffsetCollider()
    {
        collider2d.offset = rigidbody2d.velocity * Time.fixedDeltaTime;
    }

    private void SnapToPlatform(Transform platformTransform, CollisionDirection direction)
    {
        //Debug.Log(string.Format("Snapping to {0} {1}", transform.gameObject.name, direction));
        switch (direction)
        {
            case CollisionDirection.Down:
                rigidbody2d.position =
                    new Vector2(rigidbody2d.position.x,
                        platformTransform.position.y +
                        (transform.localScale.y / 2.0f + platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Up:
                rigidbody2d.position =
                    new Vector2(rigidbody2d.position.x,
                        platformTransform.position.y -
                        (transform.localScale.y / 2.0f + platformTransform.lossyScale.y / 2f));
                break;
            case CollisionDirection.Right:
                rigidbody2d.position =
                    new Vector2(
                        platformTransform.position.x -
                        (transform.localScale.x / 2.0f + platformTransform.lossyScale.x / 2f),
                        rigidbody2d.position.y);
                break;
            case CollisionDirection.Left:
                rigidbody2d.position =
                    new Vector2(
                        platformTransform.position.x +
                        (transform.localScale.x / 2.0f + platformTransform.lossyScale.x / 2f),
                        rigidbody2d.position.y);
                break;
        }
    }

    private void HandleCollision(Collision2D other)
    {
        var position = transform.position;
        var localScale = transform.localScale;
        var downCast = Physics2D
            .BoxCast(position + Vector3.down * (localScale.y / 2.0f + 0.1f), new Vector2(0.5f, 0.2f), 0f, Vector2.down,
                0f).collider;
        var upCast = Physics2D
            .BoxCast(position + Vector3.up * (localScale.y / 2.0f + 0.1f), new Vector2(0.5f, 0.2f), 0f, Vector2.up, 0f)
            .collider;
        var leftCast = Physics2D
            .BoxCast(position + Vector3.left * (localScale.x / 2.0f + 0.1f), new Vector2(0.2f, 0.5f), 0f, Vector2.left,
                0f).collider;
        var rightCast = Physics2D
            .BoxCast(position + Vector3.right * (localScale.x / 2.0f + 0.1f), new Vector2(0.2f, 0.5f), 0f,
                Vector2.right, 0f).collider;
        if (other.collider == downCast)
        {
            ground = other.collider.gameObject;
            Grounded = true;
            SnapToPlatform(other.transform, CollisionDirection.Down);
        }
        else if (other.collider == upCast)
        {
            SnapToPlatform(other.transform, CollisionDirection.Up);
        }
        else if (other.collider == leftCast)
        {
            wall = other.collider.gameObject;
            IsOnWall = IsOnWall.Left;
            SnapToPlatform(other.transform, CollisionDirection.Left);
        }
        else if (other.collider == rightCast)
        {
            wall = other.collider.gameObject;
            IsOnWall = IsOnWall.Right;
            SnapToPlatform(other.transform, CollisionDirection.Right);
        }
    }

    private enum CollisionDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public void updatePower(float value)
    {
        power = Mathf.Lerp(minPower, maxPower, value);
    }

    public void updateDirection(float angle)
    {
        direction = Quaternion.Euler(0, 0, Mathf.Lerp(0, 180, angle)) * Vector2.right;
    }
}
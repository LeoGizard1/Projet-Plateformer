using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool grounded { get; private set; } = false;

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void snapToGround(Transform groundTransform)
    {
        _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, groundTransform.position.y + 1f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(String.Format("Collision detected between : {0} and {1}",this.name,other.collider.name));
        var raycastCollider = Physics2D.Raycast(transform.position, Vector2.down, 0.6f).collider;
        Debug.Log(String.Format("Raycast and collider : {0}  {1}",raycastCollider.name,other.collider.name));
        if (other.collider == raycastCollider)
        {
            grounded = true;
            snapToGround(other.transform);
        }
    }
    
    

    private void OnCollisionExit2D(Collision2D other)
    {
        var raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        if (raycastHit.collider == null)
        {
            grounded = false;
        }
    }
}

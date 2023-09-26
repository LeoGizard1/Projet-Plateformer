using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool IsOnGround { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(String.Format("Collision detected between : {0} and {1}",this.name,other.collider.name));
        var raycastCollider = Physics2D.Raycast(transform.position, Vector2.down, 2f).collider;
        Debug.Log(String.Format("Raycast and collider : {0}  {1}",raycastCollider.name,other.collider.name));
        if (other.collider == raycastCollider)
        {
            IsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 2f);
        if (raycastHit.collider == null)
        {
            IsOnGround = false;
            GetComponent<Falling>().enabled = true;
        }
    }
}

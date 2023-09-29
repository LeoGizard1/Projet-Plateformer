using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float deceleration;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.velocity = new Vector2(0, jumpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Space) || _rigidbody2D.velocity.y < 0)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
        }

        _rigidbody2D.velocity = 
            new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, _rigidbody2D.velocity.y - deceleration * Time.deltaTime);

    }
}

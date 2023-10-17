using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class headbandScript : MonoBehaviour
{

    [SerializeField] private float speedFactor;
    [SerializeField] private float maxDeformation;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = _rigidbody.velocity;
        var directionFactor =  (Mathf.Abs(v.normalized.x) - Mathf.Abs(v.normalized.y) + 1)/2.0f;
        var xDeformation = Mathf.Lerp(-maxDeformation, maxDeformation, directionFactor) * v.magnitude / speedFactor;

        _renderer.size = new Vector2(1 + xDeformation,1);

        if (v.x > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if(v.x < 0)
        {
            transform.rotation = quaternion.Euler(new Vector3(0,Mathf.PI,0));
        }
    }
}

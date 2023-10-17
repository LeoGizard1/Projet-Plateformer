using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformationScript : MonoBehaviour
{
    
    [SerializeField] private float speedFactor;
    [SerializeField] private float maxDeformation;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = _rigidbody2D.velocity;
        var directionFactor =  (Mathf.Abs(v.normalized.x) - Mathf.Abs(v.normalized.y) + 1)/2.0f;
        var xDeformation = Mathf.Lerp(-maxDeformation, maxDeformation, directionFactor) * v.magnitude / speedFactor;

        _renderer.size = new Vector2(1 + xDeformation, 1 - xDeformation);
    }
}

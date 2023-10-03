using System;

using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private int numberAirJumps;
    private int airJumps;

    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.grounded)
        {
            airJumps = numberAirJumps;
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        } else if (Input.GetKeyDown(KeyCode.Space) && airJumps > 0)
        {
            airJumps--;
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }

        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Mathf.Clamp(velocity.y - gravity * Time.deltaTime,-maxFallSpeed,0));
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }


}

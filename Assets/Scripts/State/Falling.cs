using System;
using UnityEngine;

public class Falling : PlayerState
{
    [SerializeField] private int numberAirJumps;
    private int airJumps;

    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (_controller.grounded)
        {
            airJumps = numberAirJumps;
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        } else if (jump.WasPressedThisFrame() && airJumps > 0)
        {
            airJumps--;
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }

        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(move.ReadValue<Vector2>().x * speed, Mathf.Clamp(velocity.y - gravity * Time.deltaTime,-maxFallSpeed,0));
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }


}

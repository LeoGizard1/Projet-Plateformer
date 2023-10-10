using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWall : PlayerState
{
    [SerializeField] private float onWallFallingSpeed;

    // Update is called once per frame
    void Update()
    {
        if (!_controller.isOnWall)
        {
            this.enabled = false;
            GetComponent<Falling>().enabled = true;
            return;
        }
        else if (_controller.grounded)
        {
            this.enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }
        else if (jump.WasPressedThisFrame())
        {
            this.enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }
        if (!grab.IsPressed())
        {
            _rigidbody.velocity = new Vector2(0, -1) * onWallFallingSpeed;
        } else
        {
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }
}

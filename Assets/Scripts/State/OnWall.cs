using UnityEngine;

public class OnWall : PlayerState
{
    [SerializeField] private float onWallFallingSpeed;

    private void Update()
    {
        if (Controller.IsOnWall == IsOnWall.None)
        {
            enabled = false;
            GetComponent<Falling>().enabled = true;
            return;
        }

        if (Controller.Grounded)
        {
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }

        if (Jump.WasPressedThisFrame())
        {
            enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }

        if (!Grab.IsPressed())
            Rigidbody.velocity = new Vector2(0, -1) * onWallFallingSpeed;
        else
            Rigidbody.velocity = new Vector2(0, 0);
    }
}
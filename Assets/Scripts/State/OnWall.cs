using UnityEngine;

public class OnWall : PlayerState
{
    private void Update()
    {
        if (Controller.IsOnWall == IsOnWall.None)
        {
            Debug.Log("Falling");
            enabled = false;
            GetComponent<Falling>().enabled = true;
            return;
        }

        if (Controller.Grounded)
        {
            Debug.Log("Idle");
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }

        if (Jump.WasPressedThisFrame())
        {
            Debug.Log("Jumping");
            enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }

        Rigidbody.velocity = new Vector2(0, 0);
    }
}
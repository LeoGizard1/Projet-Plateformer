using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Falling : PlayerState
{
    [SerializeField] protected float maxFallSpeed;
    [SerializeField] protected float horizontalSpeed;

    private void Update()
    {
        if (Controller.Grounded || Controller.IsOnWall != IsOnWall.None)
        {
            Debug.Log("Idle");
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }

        var velocity = Rigidbody.velocity;
        Rigidbody.velocity = new Vector2(velocity.x, velocity.y - Controller.gravity * Time.deltaTime);
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector2.zero;
    }
}
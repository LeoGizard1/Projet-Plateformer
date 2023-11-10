using UnityEngine;

public class Falling : PlayerState
{
    [SerializeField] private int numberAirJumps;

    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float horizontalSpeed;
    private int airJumps;

    private void Update()
    {
        if (Controller.Grounded)
        {
            airJumps = numberAirJumps;
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }

        if (Controller.IsOnWall != IsOnWall.None)
        {
            airJumps = numberAirJumps;
            enabled = false;
            GetComponent<OnWall>().enabled = true;
            return;
        }

        if (Jump.WasPressedThisFrame() && airJumps > 0)
        {
            airJumps--;
            enabled = false;
            GetComponent<Jumping>().enabled = true;
            return;
        }

        var velocity = Rigidbody.velocity;
        Rigidbody.velocity = new Vector2(Move.ReadValue<Vector2>().x * horizontalSpeed,
            Mathf.Clamp(velocity.y - gravity * Time.deltaTime, -maxFallSpeed, 0));
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector2.zero;
    }
}
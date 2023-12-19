using UnityEngine;

public class Falling : PlayerState
{
    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float horizontalSpeed;

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
        Rigidbody.velocity = new Vector2(velocity.x, velocity.y - gravity * Time.deltaTime);
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector2.zero;
    }
}
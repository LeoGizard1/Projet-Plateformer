using UnityEngine;

public class Running : PlayerState
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (!Controller.Grounded)
        {
            enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Mathf.Abs(Move.ReadValue<Vector2>().x) < 0.01f)
        {
            enabled = false;
            Rigidbody.velocity = Vector2.zero;
            GetComponent<Idle>().enabled = true;
        }
        else if (Jump.WasPressedThisFrame() && Controller.Grounded)
        {
            enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
        else
        {
            Rigidbody.velocity = new Vector2(Move.ReadValue<Vector2>().x, 0) * speed;
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

public class Jumping : PlayerState
{
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalWallJumpSpeed;
    [SerializeField] private float wallJumpDeceleration;

    private int wallDirection;

    private void Update()
    {
        if (Rigidbody.velocity.y < 0)
        {
            Debug.Log("Falling");
            enabled = false;
            GetComponent<Falling>().enabled = true;
            return;
        }
        /*else if (Controller.Grounded || Controller.IsOnWall != IsOnWall.None)
        {
            Debug.Log("Idle");
            enabled = false;
            GetComponent<Idle>().enabled = true;
            return;
        }*/

        var velocity = Rigidbody.velocity;
        Rigidbody.velocity = new Vector2(velocity.x, velocity.y - gravity * Time.deltaTime);
    }

    private void OnEnable()
    {
        //Get the vector
        Vector3 direction = new Vector3(6, 10, 0);
        Rigidbody.velocity = direction;
        
    }
}
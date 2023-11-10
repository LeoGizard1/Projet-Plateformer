using UnityEngine;

public class Jumping : PlayerState
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalWallJumpSpeed;
    [SerializeField] private float wallJumpDeceleration;

    private int wallDirection;

    private void Update()
    {
        if (!Jump.IsPressed() || Rigidbody.velocity.y < 0)
        {
            enabled = false;
            GetComponent<Falling>().enabled = true;
        }

        // Setting a goal vector for the velocity so that the player can move and jump away from the wall
        var goalVelocity = new Vector2(Move.ReadValue<Vector2>().x * horizontalWallJumpSpeed, Rigidbody.velocity.y);
        Vector2 v;
        v = Vector2.MoveTowards(Rigidbody.velocity, goalVelocity, 0.2f);
        v = new Vector2(v.x, v.y - wallJumpDeceleration * Time.deltaTime);
        Rigidbody.velocity = v;
    }

    private void OnEnable()
    {
        SetWallDirection();
        Rigidbody.velocity = new Vector2(jumpSpeed * wallDirection, jumpSpeed);
    }

    public void SetWallDirection()
    {
        switch (Controller.IsOnWall)
        {
            case IsOnWall.None:
                wallDirection = 0;
                break;
            case IsOnWall.Left:
                wallDirection = 1;
                break;
            case IsOnWall.Right:
                wallDirection = -1;
                break;
        }
    }
}
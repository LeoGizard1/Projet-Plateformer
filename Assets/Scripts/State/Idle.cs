using UnityEngine;

public class Idle : PlayerState
{
    private void Update()
    {
        // If not on ground we fall
        if (!Controller.Grounded && Controller.IsOnWall == IsOnWall.None)
        {
            Debug.Log("Falling");
            enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Jump.WasPressedThisFrame() && (Controller.Grounded || Controller.IsOnWall != IsOnWall.None))
        {
            Debug.Log("Jumping");
            enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
    }

    private void OnEnable()
    {
        Rigidbody.velocity = new Vector2(0, 0);
    }
}
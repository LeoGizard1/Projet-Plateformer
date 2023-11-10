using UnityEngine;

public class Idle : PlayerState
{
    private void Update()
    {
        // If not on ground we fall
        if (!Controller.Grounded)
        {
            enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Mathf.Abs(Move.ReadValue<Vector2>().x) > 0.01f)
        {
            enabled = false;
            GetComponent<Running>().enabled = true;
        }
        else if (Jump.WasPressedThisFrame() && Controller.Grounded)
        {
            enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
    }
}
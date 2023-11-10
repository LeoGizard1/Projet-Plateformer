using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{ 
    public void OnMove(InputAction.CallbackContext value)
    {
        var inputMovement = value.ReadValue<Vector2>();
        gameObject.transform.position = inputMovement;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    protected PlayerController Controller;
    protected Rigidbody2D Rigidbody;
    protected InputAction Grab;
    protected InputAction Jump;
    protected InputAction Move;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();


        Jump = actions.FindActionMap("gameplay").FindAction("jump");
        Jump.Enable();
        Move = actions.FindActionMap("gameplay").FindAction("move");
        Move.Enable();
        Grab = actions.FindActionMap("gameplay").FindAction("grab");
        Grab.Enable();
    }
}
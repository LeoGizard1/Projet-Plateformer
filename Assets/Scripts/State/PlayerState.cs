using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerState : MonoBehaviour
{
    protected PlayerController _controller;
    protected Rigidbody2D _rigidbody;

    [SerializeField] InputActionAsset actions;
    protected InputAction jump;
    protected InputAction move;
    protected InputAction grab;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        

        jump = actions.FindActionMap("gameplay").FindAction("jump");
        jump.Enable();
        move = actions.FindActionMap("gameplay").FindAction("move");
        move.Enable();
        grab = actions.FindActionMap("gameplay").FindAction("grab");
        grab.Enable();
    }
}

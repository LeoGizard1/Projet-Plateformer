using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerState : MonoBehaviour
{
    protected PlayerController _controller;
    protected Rigidbody2D _rigidbody; // to be deleted

    private InputActionAsset actions;
    protected InputAction jump;
    protected InputAction move;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>(); // to be deleted

        actions = InputActionAsset.FromJson(File.ReadAllText("Assets/InputActions.inputactions"));
        jump = actions.FindActionMap("gameplay").FindAction("jump");
        jump.Enable();
        move = actions.FindActionMap("gameplay").FindAction("move");
        move.Enable();
    }
};

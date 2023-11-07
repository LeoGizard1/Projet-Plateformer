using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;

    private InputAction jump;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        jump = actions.FindActionMap("gameplay").FindAction("jump");
        jump.Enable();
    }

    void Update()
    {
        if (jump.WasPressedThisFrame())
        {
            OnPlay();
        }
    }
    

    public void OnPlay()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}

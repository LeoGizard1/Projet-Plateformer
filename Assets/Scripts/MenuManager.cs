using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;

    private InputAction jump;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 0;
        jump = actions.FindActionMap("gameplay").FindAction("jump");
        jump.Enable();
    }

    private void Update()
    {
        if (jump.WasPressedThisFrame()) OnPlay();
    }


    public void OnPlay()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
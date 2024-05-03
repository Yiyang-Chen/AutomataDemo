using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool showCursor = true;

    //New Input system
    private PlayerControls _inputControls;
    private InputAction move;
    private InputAction jump;

    private void Awake()
    {
        _inputControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = _inputControls.Player.Move;
        jump = _inputControls.Player.Jump;

        move.Enable();
        jump.Enable();

        SingletonManager.Get<InputMgr>().move = move;
        SingletonManager.Get<InputMgr>().jump = jump;
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    private void Update()
    {
        #region SampleCode
        // Pause Menu Panel
        if (SingletonManager.Get<InputMgr>().jump.WasPressedThisFrame())
        {
            SingletonManager.Get<EventCenter>().EventTrigger(__EVENTENUMS.Example_JumpPressed);
        }
        #endregion
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        //SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

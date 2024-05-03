using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 管理玩家输入 old input handler & new input system
/// </summary>
[ManagedSingleton]
public class InputMgr : Singleton
{
    PlayerInputController controller;

    #region inputsystemparameter
    //New Input system
    public PlayerControls _inputControls;
    public InputAction move;
    public InputAction jump;
    #endregion

    #region parameters
    private bool showCursor = true;
    public __INPUTSTATE currentInputState = __INPUTSTATE.Default;
    #endregion
    
    public InputMgr()
    {
        controller = GameObject.Find("PlayerInputController").GetComponent<PlayerInputController>();
        SingletonManager.Get<MonoManager>()?.AddUpdateListener(Update);
    }
    private void Update()
    {
        //MouseRaycastCheck();
    }

    #region mouse&cursor
    void ShowCursor()
    {
        showCursor = controller.showCursor;
        if (showCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MouseRaycastCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
        }
    }
    #endregion

    // Function to enable certain input functionalities in a particular state
    public void SwitchInputState(__INPUTSTATE inputState)
    {
        switch (inputState)
        {
            case __INPUTSTATE.Default:
                currentInputState = __INPUTSTATE.Default;
                DisableDefault(false);
                break;
            case __INPUTSTATE.Pause:
                currentInputState = __INPUTSTATE.Pause;
                DisableDefault(true);
                break;
        }
    }



    #region InputState
    public void DisableDefault(bool disable)
    {
        showCursor = disable;
        _DisablePlayerDefault(disable);
    }

    private void _DisablePlayerDefault(bool disable)
    {
        if (disable)
        {
        }

        else
        {
        }
    }
    #endregion
    
}

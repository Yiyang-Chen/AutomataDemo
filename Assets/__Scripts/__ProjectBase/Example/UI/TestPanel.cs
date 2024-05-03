using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetControl<Text>("Text").Count);
        //�Զ����¼�����
        UIMgr.AddCustomEventListener(GetControl<Button>("ButtonStart")[0], EventTriggerType.PointerEnter, (data) =>
        {
            Debug.Log("����");
        });
    }

    public void InitialFinished()
    {
        Debug.Log("initial finished");
    }
    public void ClickStart()
    {
        SingletonManager.Get<UIMgr>().GetPanel<TestPanel>(__PANELS.ExamplePanel).ClickAgain();
    }
    public void ClickClose()
    {
        SingletonManager.Get<UIMgr>().HidePanel(__PANELS.ExamplePanel);
    }
    private void ClickAgain()
    {
        Debug.Log("DoSomething on the panel");
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        Debug.Log("showed panel, initialze");
    }
    
    //�����¼�����
    protected override void OnClick(string btnName)
    {
        switch(btnName)
        {
            case "ButtonStart":
                ClickStart();
                break;
            case "ButtonEnd":
                ClickClose();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<UIMgr>().ShowPanel<TestPanel>(__PANELS.ExamplePanel, ShowPanelOver, __UI_Layer.Mid);
    }
    
    private void ShowPanelOver(TestPanel panel)
    {
        panel.InitialFinished();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}

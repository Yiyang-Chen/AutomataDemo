using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

/// <summary>
/// ����UI
/// 1.���ػ��Ƴ�һ�����
/// 2.����������ʾ�����
/// 3.�ṩ�ⲿ�������ķ���
/// 
/// Manage UI
/// 1.Show or hide a panel
/// 2.Manage all the shown panels
/// 3.Provide easy access to any panel
/// 
/// !!! IMPORTANT 
///                 The Canvas and the EventSystem should not be put into the scene.
/// !!! IMPORTANT                 
/// </summary>
[ManagedSingleton]
public class UIMgr : Singleton
{
    public Dictionary<__PANELS, BasePanel> panelDic=new Dictionary<__PANELS, BasePanel>();
    UIController controller;
    //Reference resolution
    private Vector2 _REFERENCE_RESOULTION_RATIO = new Vector2(1920, 1080);

    //2D
    //Set by necessary
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform popUp;
    private Transform system;
    private Transform dialogue;
    //canvas
    public RectTransform canvas;
    private GameObject eventSystem;
        
    public UIMgr()
    {
        //Loading
        controller = GameObject.Find("UIController").GetComponent<UIController>();
        //2DCanvas
        GameObject obj = SingletonManager.Get<ResourceMgr>()?.Load<GameObject>("_UI/__ProjectBase/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        popUp = canvas.Find("PopUp");
        system = canvas.Find("System");
        dialogue = canvas.Find("Dialogue");
        //EventSystem
        if (!GameObject.Find("EventSystem"))
            eventSystem = SingletonManager.Get<ResourceMgr>()?.Load<GameObject>("_UI/__ProjectBase/EventSystem");
        else
            eventSystem = GameObject.Find("EventSystem");
        GameObject.DontDestroyOnLoad(eventSystem);
        //Temp resolution adjustment function
        SingletonManager.Get<MonoManager>()?.AddUpdateListener(Update);
    }

    private void Update()
    {
    }

    //2DVersion get layer father
    public Transform GetLayerFather(__UI_Layer layer)
    {
        switch (layer)
        {
            case __UI_Layer.Bot:
                return this.bot;
            case __UI_Layer.Mid:
                return this.mid;
            case __UI_Layer.Top:
                return this.top;
            case __UI_Layer.Popup:
                return this.popUp;
            case __UI_Layer.Dialogue:
                return this.dialogue;
            case __UI_Layer.System:
                return this.system;
        }
        return null;
    }

    /// <summary>
    /// ����panel��������Ҫ��Ĳ���
    /// 2DVersion
    /// Load a panel to desired layer.  
    /// !!! Important: panels should be streshed - see Interact&Examine/InteractTextPanel as an example
    /// </summary>
    /// <typeparam panelEnum="T">���ű�����  The class of your panel</typeparam>
    /// <param panelEnum="panelName">���·�� panel panelEnum</param>
    /// <param panelEnum="callBack">������ɺ��������� callback function after creating your panel</param>
    /// <param panelEnum="layer">��ʾ����һ�㣬Ĭ��MID  Which layer to put panel. By default, MID.</param>
    public void ShowPanel<T>(__PANELS panelEnum, UnityAction<T> callBack=null, __UI_Layer layer = __UI_Layer.Mid) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelEnum))
        {
            panelDic[panelEnum].ShowPanel();
            if (callBack != null)
                callBack(panelDic[panelEnum] as T);
            return;
        }
        else
        {
            SingletonManager.Get<ResourceMgr>()?.LoadAsyn<GameObject>("_UI/" + controller._panelPaths[panelEnum], (o) =>
            {
                Transform father = bot;
                switch (layer)
                {
                    case __UI_Layer.Mid:
                        father = mid;
                        break;
                    case __UI_Layer.Top:
                        father = top;
                        break;
                    case __UI_Layer.Popup:
                        father = popUp;
                        break;
                    case __UI_Layer.Dialogue:
                        father = dialogue;
                        break;
                    case __UI_Layer.System:
                        father = system;
                        break;
                }

                o.transform.SetParent(father);

                o.transform.localPosition = Vector3.zero;
                o.transform.localScale = Vector3.one;

                (o.transform as RectTransform).offsetMax = Vector2.zero;
                (o.transform as RectTransform).offsetMin = Vector2.zero;

                //�õ�Ԥ�����ϵ����ű�
                //Get the script of your panel class.
                T panel = o.GetComponent<T>();
                //������崴����ɺ���߼�
                //After loaded call functions.
                panel.ShowPanel();
                if (callBack != null)
                    callBack(panel);
                //�������
                panelDic.Add(panelEnum, panel);
            });
        } 
    }

    /// <summary>
    /// 2D
    /// ɾ�����Ԥ����
    /// Delete a panel
    /// </summary>
    /// <param panelEnum="panelEnum"></param>
    public void RemovePanel(__PANELS panelEnum)
    {
        if (panelDic.ContainsKey(panelEnum))
        {
            panelDic[panelEnum].HidePanel();
            GameObject.Destroy(panelDic[panelEnum].gameObject);
            panelDic.Remove(panelEnum);
        }
    }

    /// <summary>
    /// 2D
    /// Call HidePanel Function
    /// Delete a panel
    /// </summary>
    /// <param panelEnum="panelEnum"></param>
    public void HidePanel(__PANELS panelEnum)
    {
        if (panelDic.ContainsKey(panelEnum))
        {
            panelDic[panelEnum].HidePanel();
            return;
        }
        Debug.LogError($"UIMGR: No such panel: {controller._panelPaths[panelEnum]}");
    }

    /// <summary>
    /// 2D
    /// �õ�ĳһ���Ѿ����ڵ����
    /// Get a exist panel.
    /// </summary>
    public T GetPanel<T>(__PANELS panelEnum) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelEnum))
            return panelDic[panelEnum] as T;
        return null;
    }

    /// <summary>
    /// ��װevent trigger�Զ����¼�����
    /// Allow you customize event trigger.
    /// </summary>
    /// <param panelEnum="control">�ؼ����� target</param>
    /// <param panelEnum="type">�¼����� event type</param>
    /// <param panelEnum="callBack">�¼���Ӧ���� callback</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);
        //������Լ����û���Ѿ�������¼��ˣ�һ�㲻�����
        //check existence
        trigger.triggers.Add(entry);
    }

    public override void OnReload()
    {
        if(canvas != null)
        {
            GameObject.Destroy(canvas.gameObject);
        }
        if(eventSystem != null)
        {
            GameObject.Destroy(eventSystem);
        }
    }
}

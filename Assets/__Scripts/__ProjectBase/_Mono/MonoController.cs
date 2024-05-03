using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Mono�Ĺ�����
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction startEvent;
    private event UnityAction updateEvent;

    void Start()
    {
        if (startEvent != null) startEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null) updateEvent();
    }


    public void AddStartListener(UnityAction fun)
    {
        //Debug.Log("add start fun");
        startEvent += fun;
    }

    /// <summary>
    /// ���֡�����¼��ĺ���
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    /// <summary>
    /// �Ƴ�֡�����¼��ĺ���
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

    public void _DontDestoryOnLoad(GameObject obj)
    {
        DontDestroyOnLoad(obj);
    }

    public void _Destory(Object obj)
    {
        Destroy(obj);
    }
}

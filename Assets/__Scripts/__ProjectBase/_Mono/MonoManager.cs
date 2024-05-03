using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ����u3d update�������⣬���԰Ѵ󲿷�update���뵽����ͳһupdate
/// In singleton, call update or all other MonoBehavior methods here. 
/// </summary>
[ManagedSingleton]
public class MonoManager : Singleton
{
    private MonoController controller;

    public MonoManager()
    {
        controller = GameObject.Find("MonoController").GetComponent<MonoController>();
    }

    //Awake and enable are not be able to do before controller start

    public void AddStartListener(UnityAction fun)
    {
        controller.AddStartListener(fun);
    }

    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }
    //���ֻ��������controller���еĺ���
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public void StopAllCoroutines()
    {
        controller.StopAllCoroutines();
    }

    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }

    public void StopCoroutine(string methodName)
    {
        controller.StopCoroutine(methodName);
    }

    public void DontDestoryOnLoad(GameObject obj)
    {
        controller._DontDestoryOnLoad(obj);
    }

    public void Destory(Object obj)
    {
        controller._Destory(obj);
    }


}

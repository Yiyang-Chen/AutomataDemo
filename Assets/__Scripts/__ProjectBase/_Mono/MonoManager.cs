using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 由于u3d update性能问题，可以把大部分update加入到这里统一update
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
    //这个只能运行在controller里有的函数
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

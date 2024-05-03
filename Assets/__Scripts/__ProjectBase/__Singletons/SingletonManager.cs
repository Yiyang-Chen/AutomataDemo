using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ManagedSingletonAttribute : Attribute
{
}

public class Singleton
{
    public Singleton() 
    {
        Init();
    }

    public virtual void Init()
    {

    }

    public virtual void OnReload()
    {

    }
}

public class SingletonManager
{
    private static SingletonManager _instance = new SingletonManager();
    private static Dictionary<Type, Singleton> _singletons;

    public static SingletonManager GetInstance()
    {
        return _instance;
    }

    private void RegisterSingletons()
    {
        Get<MonoManager>();
        Get<InputMgr>();
        Get<EventCenter>();
        Get<ResourceMgr>();
        Get<PoolMgr>();
        Get<SceneMgr>();
        Get<UIMgr>();
        Get<GameStateMgr>();
        Get<ModuleManager>();
        Get<EntityManager>();

    }

    public static T Get<T>() where T : Singleton
    {
        Type t = typeof(T);
        if (!_singletons.ContainsKey(t) || _singletons[t] == null)
        {
            var singAttr = (ManagedSingletonAttribute)Attribute.GetCustomAttribute(t, typeof(ManagedSingletonAttribute));
            if (singAttr != null)
            {
                Singleton singleton = (Singleton)Activator.CreateInstance(t);
                _singletons.Add(t, singleton);

                Debug.Log($"<color=lime>[SingletonManager]</color> registered singleton '{t.Name}'");
            }
            else
            {
                Debug.LogError($"<color=lime>[SingletonManager]</color> This is not a managed singleton '{t.Name}'");
                return null;
            }
        }

        return (T)_singletons[typeof(T)];
    }

    static SingletonManager() { }

    private SingletonManager()
    {
        Debug.Log("[SingletonManager] Created");
        Init();
    }

    private void Init()
    {
        _singletons = new();
        RegisterSingletons();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void OnDomainReload()
    {
        if(_instance != null)
        {
            foreach(var singleton in _singletons)
            {
                singleton.Value.OnReload();
            }
            _instance = new();
        }
        
    }
}


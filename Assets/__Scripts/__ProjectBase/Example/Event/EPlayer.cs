using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPlayer : MonoBehaviour
{
    private void OnEnable()
    {
        SingletonManager.Get<EventCenter>().RemoveEventListener<int>(__EVENTENUMS.Example_MonsterDead, MonsterDead);
        SingletonManager.Get<EventCenter>().AddEventListener<int>(__EVENTENUMS.Example_MonsterDead, MonsterDead);
    }

    public void MonsterDead(int mid)
    {
        Debug.Log("Player knows!");
    }
}

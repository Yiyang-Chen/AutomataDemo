using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMonster : MonoBehaviour
{
    public int _id = 101;
    public EPlayer player;
    public EOther[] eOthers;
    void Start()
    {
        Dead();
    }

    void Dead()
    {
        Debug.Log("Monster dead!");
        SingletonManager.Get<EventCenter>().EventTrigger<int>(__EVENTENUMS.Example_MonsterDead, _id);
    }
}

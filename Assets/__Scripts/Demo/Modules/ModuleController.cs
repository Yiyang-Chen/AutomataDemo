using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : MonoBehaviour
{
    public ShootBase defaultShoot;
    public BuffBase defaultBullet;

    public List<ModuleExcutor> excutors = new();
    public int excutiingIndex = 0;
    public float time = 0;
    Coroutine _co;

    public void InitExcutors(List<int> maxvalues)
    {
        excutors.Clear();
        foreach(int max in maxvalues)
        {
            ModuleExcutor ex = new(max);
            excutors.Add(ex);
        }
        //TODO: update UI
    }

    public void AddModule(Module module, int excutorIndex, int position)
    {
        if(excutorIndex >= excutors.Count || excutorIndex < 0)
        {
            Debug.LogError("try add module to exctuor out of range");
            return;
        }

        excutors[excutorIndex].AddModuleAt(module, position);
        //TODO: update UI
    }

    //TODO: begin tick
    public void TickStart()
    {
        if(excutors.Count == 0)
        {
            Debug.LogError("nothing to excute!!");
            return;
        }
        excutiingIndex = 0;
        time = 0;
        _co = StartCoroutine(TickTimer());
        excutors[excutiingIndex].StartTick();
    }

    //TODO: a timer to tick
    IEnumerator TickTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            excutors[excutiingIndex].OnTick();
        }
    }

    //TODO: tick next
    public void TickNext()
    {
        if (excutors.Count == 0)
        {
            Debug.LogError("nothing to excute!!");
            return;
        }
        excutiingIndex++;
        if(excutiingIndex == excutors.Count)
        {
            excutiingIndex = 0;
        }

    }

    //一场战斗结束调用
    public void GameFinished()
    {
        StopCoroutine(_co);
    }

}

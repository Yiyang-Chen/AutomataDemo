using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleExcutor
{
    public bool tickStarted = false;

    public Module[] modules;
    public int excutingIndex = 0;
    public int excutedTick= 0;
    public bool isNormal = true;

    public ModuleExcutor(int maxSize)
    {
        modules = new Module[maxSize];
        Refresh();
    }

    private void Refresh()
    {
        excutingIndex = -1;
        excutedTick = 0;
        isNormal = true;
        tickStarted = false;
    }

    public void AddModuleAt(Module module, int index)
    {
        if(index <= excutingIndex)
        {
            //TODO: tell the palyer that this module is already excuted
            return;
        }
        if(index < modules.Length && index >= 0)
        {
            modules[index] = module;
        }
    }

    public bool StartTick()
    {
        if(modules[0] == null)
        {
            Debug.Log("no module to excute");
            return false;
        }
        excutingIndex = 0;
        isNormal = SingletonManager.Get<ModuleManager>().BeginExcuteModule(modules[0]);
        //TODO: update UI if memory explode
        tickStarted = true;
        return true;
    }

    public void OnTick()
    {
        if (!tickStarted)
        {
            return;
        }
        if(excutingIndex < modules.Length && modules[excutingIndex] != null)
        {
            excutedTick++;
            //TODO: update UI
            if(excutedTick >= modules[excutingIndex].exctueTick)
            {
                SingletonManager.Get<ModuleManager>().ModuleExcuted(modules[excutingIndex], isNormal);
                excutingIndex++;
                if(excutingIndex == modules.Length || modules[excutingIndex] == null)
                {
                    ExctuedAll();
                }
                else
                {
                    isNormal = SingletonManager.Get<ModuleManager>().BeginExcuteModule(modules[excutingIndex]);
                }
            }
        }
    }

    private void ExctuedAll()
    {
        Refresh();
        SingletonManager.Get<ModuleManager>().ExcutedAll();
    }
}

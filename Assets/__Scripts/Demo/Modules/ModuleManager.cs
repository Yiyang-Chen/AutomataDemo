using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ManagedSingleton]
public class ModuleManager : Singleton
{
    public float totalMemoryUsedPrenstage = 0;
    public List<ModuleBuffBase> currentList = new();
    public List<List<ModuleBuffBase>> modulesInMemory = new();

    ModuleController controller;

    public ModuleManager()
    {
        controller = GameObject.Find("ModuleController").GetComponent<ModuleController>();
    }

    public override void OnReload()
    {
        totalMemoryUsedPrenstage = 0;
        currentList = new();
        modulesInMemory = new();
    }

    public bool BeginExcuteModule(Module module)
    {
        totalMemoryUsedPrenstage += module.memoryPrencentage;
        return totalMemoryUsedPrenstage <= 100;
    }

    public void ModuleExcuted(Module module, bool isNormal)
    {
        ModuleBuffBase result = isNormal ? module.normalBuff : module.sideBuff;
        if(result is ShootBase shootResult)
        {
            currentList.Add(shootResult);
            modulesInMemory.Add(currentList);
            currentList = new();
        }
        else
        {
            currentList.Add(result);
        }
    }

    public void ExcutedAll()
    {
        controller.TickNext();
        Shoot();
    }

    public void Shoot()
    {
        if(currentList.Count != 0)
        {
            if(currentList[currentList.Count-1] is not ShootBase)
            {
                currentList.Add(controller.defaultShoot);
            }
            modulesInMemory.Add(currentList);
        }

        if(modulesInMemory.Count == 0)
        {
            Debug.LogError("Try shoot with no module excuted");
            return;
        }

        CreateBullet(__EBulletType.Player, modulesInMemory);

        modulesInMemory = new();
        currentList = new();
        totalMemoryUsedPrenstage = 0;
    }

    public void CreateBullet(__EBulletType type, List<List<ModuleBuffBase>> modules)
    {
        var bulletSatus = modules[0];
        var modulesLeft = modules.Skip(1).ToList();
        ShootBase method = bulletSatus[bulletSatus.Count - 1] as ShootBase;
        var moduleBuffList = bulletSatus.GetRange(0, bulletSatus.Count - 1);
        List<BuffBase> buffList = new();
        if (moduleBuffList.Count == 0)
        {
            buffList.Add(controller.defaultBullet);
        }
        else
        {
            foreach (var buff in moduleBuffList)
            {
                buffList.Add((BuffBase)buff);
            }
        }
        CreateBullet(type, method, buffList, modulesLeft);
    }

    public void CreateBullet(__EBulletType type, ShootBase shootMethod, List<BuffBase> buffList, List<List<ModuleBuffBase>> modulesLeft)
    {
        SingletonManager.Get<PoolMgr>().GetObj("_Prefabs/AutomataDemo/Bullet", (go) =>
        {
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.OnSpawn(type, buffList, modulesLeft);
            shootMethod.Shoot(bullet);
        });
    }
}

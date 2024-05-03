using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string prefabPath = "";
    public __EBulletType shotType = __EBulletType.Player;
    private List<BuffBase> buffs;
    private List<List<ModuleBuffBase>> childModules;

    public void OnSpawn(__EBulletType type, List<BuffBase> buffList, List<List<ModuleBuffBase>> modulesLeft)
    {
        shotType = type;
        buffs = buffList;
        childModules = modulesLeft;
        foreach(var buff in buffs)
        {
            buff.parent = this;
        }
    }

    public void OnShot()
    {
        foreach(BuffBase buff in buffs)
        {
            buff.OnShot();
        }
    }

    private void Update()
    {
        foreach (BuffBase buff in buffs)
        {
            buff.OnFlying();
        }
    }

    public void OnHit(Entity entity)
    {
        foreach (BuffBase buff in buffs)
        {
            buff.OnHit(entity);
        }
        SingletonManager.Get<ModuleManager>().CreateBullet(shotType, childModules);
    }
}

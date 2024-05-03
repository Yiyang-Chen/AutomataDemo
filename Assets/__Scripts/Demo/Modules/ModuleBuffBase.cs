using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[CreateAssetMenu(menuName = "Buffs/BaseBuff")]*/

//这个游戏中，module分为两类，一类是修饰子弹，一类是发射方式
//对子弹的修饰是给子弹添加一些特效
//发射方式会发射修饰完成的子弹，并在击中位置按照下一个发射方式发射子弹
public abstract class ModuleBuffBase : ScriptableObject
{

}

public abstract class ShootBase : ModuleBuffBase
{
    public virtual void Shoot(Bullet bullet)
    {

    }
}

public abstract class BuffBase : ModuleBuffBase
{
    //对之后的几个发射方式生效
    public int effectTimes = 1;
    public Bullet parent;

    public virtual void OnShot()
    {

    }

    public virtual void OnFlying()
    {

    }

    public virtual void OnHit(Entity entity)
    {

    } 
}

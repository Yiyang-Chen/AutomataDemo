using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[CreateAssetMenu(menuName = "Buffs/BaseBuff")]*/

//�����Ϸ�У�module��Ϊ���࣬һ���������ӵ���һ���Ƿ��䷽ʽ
//���ӵ��������Ǹ��ӵ����һЩ��Ч
//���䷽ʽ�ᷢ��������ɵ��ӵ������ڻ���λ�ð�����һ�����䷽ʽ�����ӵ�
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
    //��֮��ļ������䷽ʽ��Ч
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

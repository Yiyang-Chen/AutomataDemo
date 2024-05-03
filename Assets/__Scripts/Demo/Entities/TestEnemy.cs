using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override string prefabPath => "_Prefabs/AutomataDemo/TestEnemy";

    public TestEnemy(float maxHp, float hp) : base(maxHp, hp)
    {

    }

    protected override void UpdateBaseMoveSpeed()
    {
        Vector3 target = SingletonManager.Get<EntityManager>().mainPlayer.transform.position;
        Vector3 current = transform.position;
        _baseMoveSpeed = GetMoveToSpeed(target, current, baseMoveSpeed);
    }

    protected override void Rotate()
    {
        RotateBySpeed(_finalSpeed);
    }

    protected override void OnHit(Bullet bullet)
    {
        Dead();
    }
}

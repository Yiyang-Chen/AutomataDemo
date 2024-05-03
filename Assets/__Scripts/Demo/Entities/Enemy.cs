using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public virtual string prefabPath
    {
        get
        {
            return "";
        }
    }
    public float baseMoveSpeed = 0.1f;

    public Enemy(float maxHp, float hp) : base(maxHp, hp)
    {

    }

    public override void Dead()
    {
        if (isActiveAndEnabled)
        {
            SingletonManager.Get<EntityManager>().EnemyDead(this);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                OnTouchPlayer();
                break;
            case "Enemy":
                break;
            case "Bullet":
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.shotType == __EBulletType.Player)
                {
                    OnHit(bullet);
                    bullet.OnHit(this);
                }
                break;
        }
    }

    protected virtual void OnTouchPlayer()
    {
        SingletonManager.Get<EntityManager>().mainPlayer.OnThouchedByEnemy();
        //TODO: anything else
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Player(float maxHp, float hp) :base(maxHp, hp)
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                break;
            case "Enemy":
                //让enemy驱动player做事，自己不用写逻辑
                break;
            case "Bullet":
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.shotType == __EBulletType.Enemy)
                {
                    OnHit(bullet);
                    bullet.OnHit(this);
                }
                break;
        }
    }

    public void OnThouchedByEnemy()
    {

    }

    protected override void OnHit(Bullet bullet)
    {
        
        //TODO: 被enemy子弹打中要做什么
    }
}

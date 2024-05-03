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
                //��enemy����player���£��Լ�����д�߼�
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
        
        //TODO: ��enemy�ӵ�����Ҫ��ʲô
    }
}

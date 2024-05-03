using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Hp
    public float _maxHP = 100;
    public float _hp = 100;

    //speed
    protected Vector3 _baseMoveSpeed = Vector3.zero;
    protected Vector3 _extraSpeed = Vector3.zero;
    protected float _speedFactor = 1;
    protected Vector3 _finalSpeed = Vector3.zero;

    public Entity(float maxHp, float hp)
    {
        _maxHP = maxHp;
        _hp = hp;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if(_hp == 0)
        {
            Dead();
        }
        Move();
        Rotate();
    }

    #region life
    public void SetHp(float hp)
    {
        _hp = Mathf.Clamp(hp, 0, _maxHP);
    }

    [Button("dead")]
    public virtual void Dead()
    {
        
    }
    #endregion life

    #region Pool
    public virtual void OnSpawned(float maxHp, float hp)
    {
        _maxHP = maxHp;
        _hp = hp;
        _baseMoveSpeed = Vector3.zero;
        _extraSpeed = Vector3.zero;
        _speedFactor = 1;
        _finalSpeed = Vector3.zero;
    }

    public virtual void OnPooled()
    {
        _baseMoveSpeed = Vector3.zero;
        _extraSpeed = Vector3.zero;
        _speedFactor = 1;
        _finalSpeed = Vector3.zero;
    }
    #endregion pool

    #region Move & Speed
    protected virtual void Move()
    {
        UpdateDeltaSpeed();
        transform.position = transform.position + _finalSpeed * Time.deltaTime;
    }

    protected virtual void UpdateBaseMoveSpeed()
    {
        _baseMoveSpeed = Vector3.zero;
    }

    protected virtual void UpdateExtraMoveSpeed()
    {
        _extraSpeed = Vector3.zero;
    }

    protected void UpdateDeltaSpeed()
    {
        UpdateBaseMoveSpeed();
        UpdateExtraMoveSpeed();
        _finalSpeed = (_baseMoveSpeed + _extraSpeed) * _speedFactor;
    }

    public virtual void SetSpeedFactor(float speedfactor)
    {
        _speedFactor = speedfactor;
    }

    protected Vector3 GetMoveToSpeed(Vector3 targetPosition, Vector3 currentPosition, float moveSpeed)
    {
        Vector3 direction = (targetPosition - currentPosition).normalized;

         return direction * moveSpeed;
    }
    #endregion Move & Speed

    #region Direction
    protected virtual void Rotate()
    {

    }

    protected void RotateBySpeed(Vector3 speed)
    {
        float targetAngle = 0f;
        if (speed.magnitude > 0.01f)
        {
            targetAngle = Mathf.Atan2(speed.y, speed.x) * Mathf.Rad2Deg;
        }

        targetAngle -= 90;

        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
    #endregion Direction

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected virtual void OnHit(Bullet bullet)
    {

    }
}

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IMoveable, IAttackerable
{
    enum EnemyState
    {
        Patrol,
        Move,
        Attack
    };

    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _attackDistance = 1.0f;
    [SerializeField] private float _provocationDistance = 20.0f;
    [SerializeField] private float _patrolRadius = 2.0f;

    private Vector3 _startPosition;
    private Vector3 _nextPosition;

    private Player _player;

    private Health _hp;

    private bool _isCanAttack = true;

    public void ApplyDamage(float damage)
    {
        _hp.ChangeHealth(damage);
    }

    public float Attack()
    {
        if(_isCanAttack)
        {
            StartCoroutine(AttackWithDelay());
        }
        return _damage;
    }

    private IEnumerator AttackWithDelay()
    {
        _isCanAttack = false;
        _player.ApplyDamage(_damage);
        yield return new WaitForSeconds(_attackDelay);
        _isCanAttack = true;
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, _nextPosition) < 0.1f)
        {
            _nextPosition = GetRandomPointInCircle(_startPosition, _patrolRadius);
        }
        transform.position = Vector2.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);
    }

    private EnemyState GetNextState(Player _player)
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _provocationDistance)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) > _attackDistance)
            {
                return EnemyState.Move;
            }
            else
            {
                return EnemyState.Attack;
            }
        }
        return EnemyState.Patrol;
    }

    private void WhatToNeedDo(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Patrol:
                {
                    Patrol();
                    break;
                }
            case EnemyState.Move:
                {
                    Move();
                    break;
                }
            case EnemyState.Attack:
                {
                    Attack();
                    break;
                }

        }
    }

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _hp = GetComponent<Health>();
        _startPosition = transform.position;
        _nextPosition = GetRandomPointInCircle(_startPosition, _patrolRadius);
    }

    private Vector3 GetRandomPointInCircle(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, 360f);
        Vector3 position;
        position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        position.z = center.z;
        return position;
    }

    private void Update()
    {
        WhatToNeedDo(GetNextState(_player));
    }

}

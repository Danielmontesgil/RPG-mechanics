using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class RangeAttackCooldown : ICooldownable
    {
        [SerializeField] private int id;
        [SerializeField] private float cooldownDuration;

        public int Id => id;

        public float CooldownDuration => cooldownDuration;
    }

    public bool _canMove = true;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 2;
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Collider2D _meleeAttackCollider;
    [SerializeField] private Transform _bow;
    [SerializeField] private RangeAttackCooldown _rangeAttackCooldown;

    private Vector2 _movement;
    private Vector2 _lookDirection = Vector2.down;
    private Vector3 _localScale = Vector3.one;
    private Vector2 _currentVelocity;

    private void FixedUpdate()
    {
        if (_canMove)
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, _movement * _moveSpeed / 10, ref _currentVelocity, _acceleration, _moveSpeed);

            // rb.position = rb.position + _movement * _moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.CurrentGameState is GameGameState && _canMove)
        {
            _movement = context.ReadValue<Vector2>();

            if(Mathf.Abs(_movement.x) > Mathf.Abs(_movement.y))
            {  
                _movement.y = 0;
                _lookDirection = Vector2.zero;
                _lookDirection = _movement;
            }
            else if(Mathf.Abs(_movement.y) > Mathf.Abs(_movement.x))
            {
                _movement.x = 0;
                _lookDirection = Vector2.zero;
                _lookDirection = _movement;
            }

            _animator.SetFloat("Horizontal", _movement.sqrMagnitude != 0 ? _movement.x : _lookDirection.x);
            _animator.SetFloat("Vertical", _movement.sqrMagnitude != 0 ? _movement.y : _lookDirection.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);

            _spriteRenderer.flipX = _movement.x < 0 || _lookDirection.x < 0;
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool("Attack", true);

            _meleeAttackCollider.gameObject.SetActive(true);
            _meleeAttackCollider.transform.localPosition = _lookDirection / 2;
            if (Mathf.Abs(_lookDirection.y) > Mathf.Abs(_lookDirection.x))
            {
                _meleeAttackCollider.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
        }

        if (context.canceled)
        {
            _animator.SetBool("Attack", false);
            _meleeAttackCollider.gameObject.SetActive(false);
            _meleeAttackCollider.transform.localPosition = Vector3.zero;
            _meleeAttackCollider.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    public async void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!CooldownManager.Instance.IsOnCooldown(_rangeAttackCooldown.Id))
            {
                _canMove = false;
                _animator.SetBool("RangeAttack", true);
                _bow.localPosition = _lookDirection / 3;
                CooldownManager.Instance.PutOnCooldown(_rangeAttackCooldown);

                await UniTask.Delay((int)(Env.TIME_TO_INSTANTIATE_BULLET * 1000));

                var bullet = PoolManager.Instance.GetObject<BulletController>(Env.BULLET_PREFAB);
                bullet.transform.position = _bow.position;
                bullet.Init(_lookDirection);

                _animator.SetBool("RangeAttack", false);
                _bow.localPosition = Vector3.zero;

                await UniTask.Delay((int)(Env.PLAYER_RANGE_ATTACK_DURATION * 1000));

                _canMove = true;        
            }
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {

    }
}

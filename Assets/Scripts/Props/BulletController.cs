using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private Vector2 _bulletDir;

    public void Init(Vector2 bulletDir)
    {
        _bulletDir = bulletDir;
        StartCoroutine(BulletLifeTimeRoutine());
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _bulletDir * _speed * Time.deltaTime);
    }

    IEnumerator BulletLifeTimeRoutine()
    {
        yield return new WaitForSeconds(5f);

        PoolManager.Instance.ReleaseObject(Env.BULLET_PREFAB, gameObject);
    }
}

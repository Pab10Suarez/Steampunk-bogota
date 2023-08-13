using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBala : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _cannon;
    private float _bulletSpeed = 5f;
    [SerializeField] private float destructionTime;

    void Start()
    {
        StartCoroutine(SpawnBulletRoutine());
    }

    IEnumerator SpawnBulletRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f, .4f));
            GameObject newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();

            Vector2 bulletDirection = _cannon.right;
            bulletRigidbody.velocity = bulletDirection * _bulletSpeed * -1;

            yield return new WaitForSeconds(destructionTime);
            Destroy(newBullet);
        }
    }
}

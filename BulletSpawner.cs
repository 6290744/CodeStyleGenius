using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _shootCooldown;

    private bool _isShooting;
    
    private void Start()
    {
        if (_prefab != null)
        {
            _isShooting = true;
        
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_shootCooldown);
        
        while (_isShooting)
        {
            Bullet bullet = Instantiate(_prefab, transform.position, Quaternion.identity);
            bullet.Attack(_target.position);

            yield return waitForSeconds;
        }
    }
} 

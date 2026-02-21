using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Attack(Vector3 direction)
    {
        SetDirection(direction);
        AddVelocity();
    }
    
    private void SetDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction - transform.position);
    }

    private void AddVelocity()
    {
        _rigidbody.linearVelocity = Vector3.forward * _bulletSpeed;
    }
}

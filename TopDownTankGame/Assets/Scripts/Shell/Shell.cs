using UnityEngine;

public class Shell : MonoBehaviour
{
    [field: SerializeField] private int _speed = 25;

    private int _damage = 25;
    private LayerMask _targetMask;

    public void Launch(Vector3 direction, LayerMask targetMask, int damage = 25) // float speed
    {
        GetComponent<Rigidbody>().velocity = direction * _speed;

        _damage = damage;
        _targetMask = targetMask;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out IDamagable target) && 1 << collision.gameObject.layer == _targetMask.value)
            target.GetDamage(_damage);

        if (collision.TryGetComponent(out DestroyableEnvironment hit))
            hit.DestroyObject();

        Destroy(gameObject);
    }
}

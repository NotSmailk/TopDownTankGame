using UnityEngine;
public class Enemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] private GameObject _main;
    [field: SerializeField] private GameObject _tower;
    [field: SerializeField] public Transform _shootPoint;

    private float _health;
    private float _maxHealth;
    private string _name;

    private EnemyTowerRotation _towerRotation;
    private EnemyDetection _detection;
    private EnemyMovement _movement;
    private EnemyShoot _shoot;

    public EnemyFactory OriginFactory { get; set; }
    public (float, float, string) EnemyInfo => (_maxHealth, _health, _name);

    public void Initialize(float health, string name, DetectionParametres detection, CannonParametres cannon)
    {
        _maxHealth = _health = health;
        _name = name;

        _towerRotation = new EnemyTowerRotation();
        _detection = new EnemyDetection();
        _movement = new EnemyMovement();
        _shoot = new EnemyShoot();

        _towerRotation.Initialize(_detection, _tower);
        _detection.Initialize(detection, _tower, transform);
        _movement.Initialize(_detection, _towerRotation, GetComponent<Rigidbody>(), _tower, gameObject);
        _shoot.Initialize(_detection, _movement, _towerRotation, cannon, _shootPoint);
    }

    public bool GameUpdate()
    {
        if (_health <= 0)
        {
            OriginFactory.Reclaim(this);

            return false;
        }

        _towerRotation.GameUpdate();
        _detection.GameUpdate();
        _movement.GameUpdate();
        _shoot.GameUpdate();

        return true;
    }

    public void DestroyTank()
    {
        Destroy(gameObject);
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        Game.UpdateEnemyInfo(this);
    }
}

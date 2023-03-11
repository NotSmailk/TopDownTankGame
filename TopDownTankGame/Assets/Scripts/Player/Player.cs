using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [field: SerializeField] private GameObject _main;
    [field: SerializeField] private GameObject _tower;
    [field: SerializeField] public Transform _shootPoint;
    [field: SerializeField] private Camera _camera;

    private float _health;
    private float _maxHealth;

    private PlayerCameraController _cameraController;
    private PlayerTowerRotation _towerRotation;
    private PlayerMovement _movement;
    private PlayerShoot _shoot;

    public PlayerFactory OriginFactory { get; set; }
    public (float, float) HealthInfo => (_maxHealth, _health);

    public void Initialize(float health, CannonParametres cannon)
    {
        _maxHealth = _health = health; 
        Game.UpdatePlayerInfo(this);
        _camera.transform.parent = transform.parent;

        _cameraController = new PlayerCameraController();
        _towerRotation = new PlayerTowerRotation();
        _movement = new PlayerMovement();
        _shoot = new PlayerShoot();

        _shoot.Initialize(_movement, _towerRotation, cannon, _shootPoint);
        _movement.Initialize(GetComponent<Rigidbody>(), _tower, gameObject);
        _towerRotation.Initialize(_camera, _tower);
        _cameraController.Initialize(_camera, transform);
    }

    public void GameUpdate()
    {
        if (_health <= 0)
        {
            OriginFactory.Reclaim(this);
            Game.Defeat();
        }

        _cameraController.GameUpdate();
        _towerRotation.GameUpdate();
        _movement.GameUpdate();
        _shoot.GameUpdate();
    }

    public void DestroyTank()
    {
        Destroy(_movement.MainModel);
        Game.Defeat();
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        Game.UpdatePlayerInfo(this);
    }
}

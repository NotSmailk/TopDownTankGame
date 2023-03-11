using UnityEngine;

public class EnemyShoot : CannonShoot
{
    private EnemyDetection _detection;
    private EnemyMovement _movement;
    private EnemyTowerRotation _rotation;

    public void Initialize(EnemyDetection detection, EnemyMovement movement, EnemyTowerRotation rotation, CannonParametres parametres, Transform shootPoint)
    {
        _detection = detection;
        _movement = movement;
        _rotation = rotation;
        _parametres = parametres;
        _shootPoint = shootPoint;
    }

    public void GameUpdate()
    {
        if (_detection.TargetInShootDistance())
        {
            Shoot(_movement.GetTowerDirection(), Quaternion.Euler(_rotation.GetTowerEulerAngles()));
        }
    }
}

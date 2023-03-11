using UnityEngine;

public class PlayerShoot : CannonShoot
{
    private PlayerMovement _playerMovement;
    private PlayerTowerRotation _playerTowerRotation;

    public void Initialize(PlayerMovement playerMovement, PlayerTowerRotation playerTowerRotation, CannonParametres parametres, Transform shootPoint)
    {
        _playerMovement = playerMovement;
        _playerTowerRotation = playerTowerRotation;
        _parametres = parametres;
        _shootPoint = shootPoint;
    }

    public void GameUpdate()
    {
        if (GameInputs.GetShoot())
        {
            Shoot(_playerMovement.TowerDirection, Quaternion.Euler(_playerTowerRotation.GetTowerEulerAngles()));
        }
    }
}

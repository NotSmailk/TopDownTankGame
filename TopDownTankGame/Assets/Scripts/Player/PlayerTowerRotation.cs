using UnityEngine;

public class PlayerTowerRotation : TowerRotation
{
    private Camera _camera;

    public void Initialize(Camera camera, GameObject tower)
    {
        _camera = camera;
        _tower = tower;
    }

    public void GameUpdate()
    {
        RotateTower(GetWorldMousePosition());

        #if UNITY_EDITOR

        Debug.DrawLine(_tower.transform.position, _tower.transform.position + _tower.transform.right * 5, Color.green);

        #endif
    }

    private Vector3 GetWorldMousePosition()
    {
        return _camera.ScreenToWorldPoint(GameInputs.GetMousePosition() + Vector3.forward * _camera.transform.position.y);
    }
}

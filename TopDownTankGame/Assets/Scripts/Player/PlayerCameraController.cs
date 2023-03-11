using UnityEngine;

public class PlayerCameraController
{
    [field: SerializeField] private float _heightBetweenCameraAndPlayer = 15f;

    private Camera _camera;
    private Transform _transform;

    public void Initialize(Camera camera, Transform transform)
    {
        _camera = camera;
        _transform = transform;

        AdjustCameraPosition();
        AdjustCameraRotation();
    }

    public void GameUpdate()
    {
        AdjustCameraPosition();
    }

    private void AdjustCameraPosition()
    {
        Vector3 newCameraPosition = new Vector3(GetPlayerPosition().x, _heightBetweenCameraAndPlayer, GetPlayerPosition().z);

        _camera.transform.position = newCameraPosition;
    }

    private void AdjustCameraRotation()
    {
        _camera.transform.LookAt(_transform);
    }

    private Vector3 GetPlayerPosition()
    {
        return _transform.position;
    }
}

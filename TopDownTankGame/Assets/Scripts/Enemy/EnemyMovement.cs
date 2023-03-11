using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement
{
    private float _speed = 5f;
    private float _rotationSpeed = 5f;
    private GameObject _main;
    private GameObject _tower;
    private Rigidbody _rigidbody;
    private EnemyDetection _detection;
    private EnemyTowerRotation _rotation;
    private Vector3 _tankDirection;

    #region MainMethods

    public void Initialize(EnemyDetection detection, EnemyTowerRotation rotation, Rigidbody rigidbody, GameObject tower, GameObject main)
    {
        _detection = detection;
        _rotation = rotation;
        _rigidbody = rigidbody;
        _tower = tower;
        _main = main;
    }

    public void GameUpdate()
    {
        if (_detection.Target == null)
            return;

        RotateTankBody();
        MoveTankBody();
    }

    #endregion MainMethods

    #region GetMethods

    public Vector3 GetTowerDirection()
    {
        return _tower.transform.right.normalized;
    }

    private Vector3 GetTankDirection(float towerAngleY, float mainAngleY)
    {
        towerAngleY = Mathf.Abs(towerAngleY - mainAngleY);

        if (towerAngleY > 90.0f && towerAngleY < 270.0f)
            return _main.transform.right.normalized * -1;

        return _main.transform.right.normalized;
    }

    #endregion

    #region MovementMethods

    private void MoveTankBody()
    {
        if (!_detection.IsChasingTarget)
            return;

        _rigidbody.velocity = _tankDirection * _speed;
    }

    private void RotateTankBody()
    {
        if (!_detection.IsChasingTarget)
            return;

        Vector3 playerRotation = _main.transform.rotation.eulerAngles;
        float targetYAngle = _rotation.GetYAngleBetweenTargetAndObject(_detection.GetTargetPosition(), _main);
        float yAngle = Mathf.MoveTowardsAngle(playerRotation.y, -targetYAngle, _rotationSpeed * Time.deltaTime);
        Quaternion newPlayerRotation = Quaternion.Euler(playerRotation.x, yAngle, playerRotation.z);

        _main.transform.rotation = newPlayerRotation;
        _tankDirection = GetTankDirection(_tower.transform.rotation.eulerAngles.y, newPlayerRotation.eulerAngles.y);

        #if UNITY_EDITOR
        int horizontal = targetYAngle > 0 ? -1 : 1;
        Debug.DrawLine(_main.transform.position, _main.transform.position + _main.transform.forward * horizontal * 5f, Color.blue);
        Debug.DrawLine(_main.transform.position, _main.transform.position + _tankDirection * 5f, Color.red);
        #endif
    }

    #endregion MovementMethods
}

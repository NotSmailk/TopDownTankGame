using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement
{
    private int _speed = 5;
    private int _rotationSpeed = 90;
    private GameObject _main;
    private GameObject _tower;
    private Rigidbody _rigidbody;
    private Vector3 _tankDirection;
    private bool _isInversedMoving = false;

    public GameObject MainModel => _main;
    public Vector3 TowerDirection => _tower.transform.right.normalized;

    public void Initialize(Rigidbody rigidbody, GameObject tower, GameObject main)
    {
        _rigidbody = rigidbody;
        _tower = tower;
        _main = main;
    }

    public void GameUpdate()
    {
        RotateTankBody();

        MoveTankBody();
    }

    private void MoveTankBody()
    {
        float vertical = GameInputs.GetVerticalAxis();

        _rigidbody.velocity = _tankDirection * _speed * vertical;
    }

    private void RotateTankBody()
    {
        Vector3 playerRotation = _main.transform.rotation.eulerAngles;

        float vertical = GameInputs.GetVerticalAxis() > -1 ? 1 : -1;
        float horizontal = GameInputs.GetHorizontalAxis();

        float yAngle = Mathf.MoveTowardsAngle(playerRotation.y, playerRotation.y + 90f * horizontal * vertical, _rotationSpeed * Time.deltaTime);

        Quaternion newPlayerRotation = Quaternion.Euler(playerRotation.x, yAngle, playerRotation.z);

        _main.transform.rotation = newPlayerRotation;

        _tankDirection = GetTankDirection(_tower.transform.rotation.eulerAngles.y, newPlayerRotation.eulerAngles.y);

        #if UNITY_EDITOR

        horizontal = _isInversedMoving ? horizontal : -horizontal;

        Debug.DrawLine(_main.transform.position, _main.transform.position + _main.transform.forward * horizontal * 5f, Color.blue);

        Debug.DrawLine(_main.transform.position, _main.transform.position + _tankDirection * 5f, Color.red);

        #endif
    }

    private Vector3 GetTankDirection(float yTowerAngle, float yBodyAngle)
    {
        yTowerAngle = Mathf.Abs(yTowerAngle - yBodyAngle);

        if (_isInversedMoving)
        {
            if (yTowerAngle > 320.0f || yTowerAngle < 40.0f)
            {
                _isInversedMoving = false;
                return _main.transform.right.normalized;
            }

            return _main.transform.right.normalized * -1;
        }

        if (yTowerAngle > 130.0f && yTowerAngle < 230.0f)
        {
            _isInversedMoving = true;
            return _main.transform.right.normalized * -1;
        }

        return _main.transform.right.normalized;
    }
}

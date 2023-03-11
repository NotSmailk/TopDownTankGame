using UnityEngine;

public class TowerRotation
{
    protected GameObject _tower;
    protected float _rotationSpeed = 5f;

    #region RotateMethods

    protected void RotateTower(Vector3 targetPosition)
    {
        Quaternion newRotation = Quaternion.Euler(GetRotationAngles(targetPosition));
        _tower.transform.rotation = Quaternion.Lerp(_tower.transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

    #endregion RotateMethods

    #region GetMethods

    protected Vector3 GetRotationAngles(Vector3 targetPosition)
    {
        float angle = GetYAngleBetweenTargetAndObject(targetPosition, _tower);
        return new Vector3(GetTowerEulerAngles().x, -angle, GetTowerEulerAngles().z);
    }

    public float GetYAngleBetweenTargetAndObject(Vector3 targetPosition, GameObject mainObject)
    {
        float directionX = targetPosition.x - mainObject.transform.position.x;
        float directionY = targetPosition.z - mainObject.transform.position.z;

        float angle = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;
        return angle;
    }

    public Vector3 GetTowerEulerAngles()
    {
        return _tower.transform.rotation.eulerAngles;
    }

    #endregion GetMethods
}

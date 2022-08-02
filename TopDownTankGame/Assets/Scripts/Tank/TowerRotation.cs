using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    [field: SerializeField] protected GameObject m_tower;
    [field: SerializeField] protected float m_rotationSpeed = 5f;

    #region RotateMethods

    protected void RotateTower(Vector3 targetPosition)
    {
        Quaternion newRotation = Quaternion.Euler(GetRotationAngles(targetPosition));

        m_tower.transform.rotation = Quaternion.Lerp(m_tower.transform.rotation, newRotation, m_rotationSpeed * Time.deltaTime);
    }

    #endregion RotateMethods

    #region GetMethods

    protected Vector3 GetRotationAngles(Vector3 targetPosition)
    {
        float angle = GetYAngleBetweenTargetAndObject(targetPosition, m_tower);

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
        return m_tower.transform.rotation.eulerAngles;
    }

    #endregion GetMethods
}

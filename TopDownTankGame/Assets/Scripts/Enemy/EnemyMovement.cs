using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [field: SerializeField] private float m_speed = 5f;
    [field: SerializeField] private float m_rotationSpeed = 5f;
    [field: SerializeField] private GameObject m_main;
    [field: SerializeField] private GameObject m_tower;

    private Rigidbody m_rigidbody;
    private EnemyComponents m_enemyComponents;
    private Vector3 m_tankDirection;
    private bool m_isInversedMoving = false;

    public GameObject MainModel { get => m_main; }
    public GameObject Tower { get => m_tower; }

    #region MainMethods

    private void Start()
    {
        m_enemyComponents = GetComponent<EnemyComponents>();

        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (m_enemyComponents.EnemyDetection.Target == null)
            return;

        RotateTankBody();

        MoveTankBody();
    }

    #endregion MainMethods

    #region GetMethods

    public Vector3 GetTowerDirection()
    {
        return m_tower.transform.right.normalized;
    }

    private Vector3 GetTankDirection(float yTowerAngle, float yBodyAngle)
    {
        yTowerAngle = Mathf.Abs(yTowerAngle - yBodyAngle);

        if (yTowerAngle > 90.0f && yTowerAngle < 270.0f)
        {
            return m_main.transform.right.normalized * -1;
        }

        return m_main.transform.right.normalized;
    }

    #endregion

    #region MovementMethods

    private void MoveTankBody()
    {
        if (!m_enemyComponents.EnemyDetection.IsChasingTarget)
            return;

        m_rigidbody.velocity = m_tankDirection * m_speed;
    }

    private void RotateTankBody()
    {
        if (!m_enemyComponents.EnemyDetection.IsChasingTarget)
            return;

        Vector3 playerRotation = m_main.transform.rotation.eulerAngles;

        float targetYAngle = m_enemyComponents.EnemyTowerRotation.GetYAngleBetweenTargetAndObject(m_enemyComponents.EnemyDetection.GetTargetPosition(), m_main);

        float yAngle = Mathf.MoveTowardsAngle(playerRotation.y, -targetYAngle, m_rotationSpeed * Time.deltaTime);

        Quaternion newPlayerRotation = Quaternion.Euler(playerRotation.x, yAngle, playerRotation.z);

        m_main.transform.rotation = newPlayerRotation;

        m_tankDirection = GetTankDirection(m_tower.transform.rotation.eulerAngles.y, newPlayerRotation.eulerAngles.y);

        #region Debug

        int horizontal = targetYAngle > 0 ? -1 : 1;

        Debug.DrawLine(m_main.transform.position, m_main.transform.position + m_main.transform.forward * horizontal * 5f, Color.blue);

        Debug.DrawLine(m_main.transform.position, m_main.transform.position + m_tankDirection * 5f, Color.red);

        #endregion Debug
    }

    #endregion MovementMethods
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField, Range(1, 20)] private int m_speed = 5;
    [field: SerializeField, Range (5, 90)] private int m_rotationSpeed = 5;
    [field: SerializeField] private GameObject m_main;
    [field: SerializeField] private GameObject m_tower;

    private PlayerComponents m_playerComponents;
    private Rigidbody m_rigidbody;
    private Vector3 m_tankDirection;
    private bool m_isInversedMoving = false;

    public GameObject MainModel { get => m_main; }

    #region MainMethods

    private void Start()
    {
        m_playerComponents = GetComponent<PlayerComponents>();

        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateTankBody();

        MoveTankBody();
    }

    #endregion MainMethodss

    #region GetMethods

    public Vector3 GetTowerDirection()
    {
        return m_tower.transform.right.normalized;
    }

    #endregion

    #region CalculatingMethods

    private void MoveTankBody()
    {
        float vertical = m_playerComponents.PlayerInputs.GetVerticalAxis();

        m_rigidbody.velocity = m_tankDirection * m_speed * vertical;
    }

    private void RotateTankBody()
    {
        Vector3 playerRotation = m_main.transform.rotation.eulerAngles;

        float vertical = m_playerComponents.PlayerInputs.GetVerticalAxis() > -1 ? 1 : -1;
        float horizontal = m_playerComponents.PlayerInputs.GetHorizontalAxis();

        float yAngle = Mathf.MoveTowardsAngle(playerRotation.y, playerRotation.y + 90f * horizontal * vertical, m_rotationSpeed * Time.deltaTime);

        Quaternion newPlayerRotation = Quaternion.Euler(playerRotation.x, yAngle, playerRotation.z);

        m_main.transform.rotation = newPlayerRotation;

        m_tankDirection = GetTankDirection(m_tower.transform.rotation.eulerAngles.y, newPlayerRotation.eulerAngles.y);

        #region Debug

        horizontal = m_isInversedMoving ? horizontal : -horizontal;

        Debug.DrawLine(m_main.transform.position, m_main.transform.position + m_main.transform.forward * horizontal * 5f, Color.blue);

        Debug.DrawLine(m_main.transform.position, m_main.transform.position + m_tankDirection * 5f, Color.red);

        #endregion Debug
    }

    private Vector3 GetTankDirection(float yTowerAngle, float yBodyAngle)
    {
        yTowerAngle = Mathf.Abs(yTowerAngle - yBodyAngle);

        if (m_isInversedMoving)
        {
            if (yTowerAngle > 320.0f || yTowerAngle < 40.0f)
            {
                m_isInversedMoving = false;

                return m_main.transform.right.normalized;
            }

            return m_main.transform.right.normalized * -1;
        }

        if (yTowerAngle > 130.0f && yTowerAngle < 230.0f)
        {
            m_isInversedMoving = true;

            return m_main.transform.right.normalized * -1;
        }

        return m_main.transform.right.normalized;
    }

    #endregion
}

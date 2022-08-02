using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [field: Header("Detection settings"), Space(5)]
    [field: SerializeField] private float m_detectionSphereRadius = 10f;
    [field: SerializeField] private float m_radiusAtObservation = 15f;
    [field: SerializeField] private float m_detectionViewRadius = 15f;
    [field: SerializeField, Range(0, 360)] private float m_detectionViewAngle = 15f;
    [field: SerializeField, Range(0, 360)] private float m_shootViewAngle = 15f;

    [field: Header("LayerMasks"), Space(5)]
    [field: SerializeField] private LayerMask m_detectionLayer;
    [field: SerializeField] private LayerMask m_obstructionLayer;

    private GameObject m_target;
    private GameObject m_tower;
    private bool m_isChasingTarget = false;
    private float m_shootDistanceOffset = 3f;

    public bool IsChasingTarget { get => m_isChasingTarget; }
    public float DetectionSphereRadius { get => m_detectionSphereRadius; }
    public float RadiusAtObservation { get => m_radiusAtObservation; }
    public float DetectionViewRadius { get => m_detectionViewRadius; }
    public float DetectionViewAngle { get => m_detectionViewAngle; }
    public GameObject Target { get => m_target; }

    private void Start()
    {
        m_tower = GetComponent<EnemyComponents>().EnemyMovement.Tower;
    }

    private void Update()
    {
        GetTarget();
    }

    #region DetectionMethods

    public void DetectObjectsInRadius()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, m_detectionSphereRadius, m_detectionLayer);

        foreach (var detectedObject in detectedObjects)
        {
            if (m_target == null)
                m_target = detectedObject.gameObject;

            return;
        }

        if (m_target != null)
            m_target = null;
    }

    public void DetectObjectAtFieldOfView()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, m_detectionViewRadius, m_detectionLayer);

        foreach (var detectedObject in detectedObjects)
        {
            Vector3 directionToTarget = (detectedObject.transform.position - transform.position).normalized;

            if (Vector3.Angle(m_tower.transform.right, directionToTarget) < m_detectionViewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(detectedObject.transform.position, transform.position);

                if (Physics.Raycast(m_tower.transform.position, directionToTarget, distanceToTarget, m_obstructionLayer))
                {
                    m_isChasingTarget = false;

                    m_target = null;

                    return;
                }

                if (m_target == null)
                    m_target = detectedObject.gameObject;

                m_isChasingTarget = distanceToTarget > m_radiusAtObservation;

                return;
            }
        }

        m_isChasingTarget = false;

        m_target = null;

        DetectObjectsInRadius();
    }

    public bool TargetInShootDistance()
    {
        if (m_target == null)
            return false;

        Vector3 directionToTarget = (m_target.transform.position - transform.position).normalized;

        if (Vector3.Angle(m_tower.transform.right, directionToTarget) < m_shootViewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(m_target.transform.position, transform.position);

            return distanceToTarget <= m_radiusAtObservation + m_shootDistanceOffset;
        }

        return false;
    }

    #endregion DetectionMethods

    #region GetMethods

    public Vector3 GetTargetPosition()
    {
        if (m_target == null)
            return transform.position;

        return m_target.transform.position;
    }

    public GameObject GetTarget()
    {
        DetectObjectAtFieldOfView();

        return m_target;
    }

    #endregion GetMethods
}

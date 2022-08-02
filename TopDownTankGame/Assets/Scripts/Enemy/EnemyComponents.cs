using UnityEngine;

[RequireComponent(typeof(EnemyTowerRotation))]
[RequireComponent(typeof(EnemyDetection))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(EnemyShoot))]
public class EnemyComponents : MonoBehaviour
{
    [field: HideInInspector] public EnemyTowerRotation EnemyTowerRotation;
    [field: HideInInspector] public EnemyDetection EnemyDetection;
    [field: HideInInspector] public EnemyMovement EnemyMovement;
    [field: HideInInspector] public EnemyStatus EnemyStatus;
    [field: HideInInspector] public EnemyShoot EnemyShoot;

    [field: SerializeField] private GameObject m_destroyedTank;

    private void Awake()
    {
        EnemyTowerRotation = GetComponent<EnemyTowerRotation>();

        EnemyDetection = GetComponent<EnemyDetection>();

        EnemyMovement = GetComponent<EnemyMovement>();

        EnemyStatus = GetComponent<EnemyStatus>();

        EnemyShoot = GetComponent<EnemyShoot>();

        LinkChilds(EnemyMovement.MainModel.transform);
    }

    private void LinkChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Collider>() == null)
                continue;

            child.gameObject.AddComponent<LinkToStatus>();

            child.GetComponent<LinkToStatus>().Status = EnemyStatus;

            LinkChilds(child);
        }
    }

    public void DestroyTank()
    {
        Destroy(gameObject);

        GameObject destroyed = Instantiate(m_destroyedTank, EnemyMovement.MainModel.transform.position + Vector3.up, EnemyMovement.MainModel.transform.rotation);

        destroyed.transform.localScale = EnemyMovement.MainModel.transform.localScale;
    }
}

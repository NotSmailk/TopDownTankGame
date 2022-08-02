using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
[RequireComponent(typeof(PlayerTowerRotation))]
[RequireComponent(typeof(PlayerGameInfoUI))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerComponents : MonoBehaviour
{
    [field: HideInInspector] public PlayerCameraController PlayerCameraController;
    [field: HideInInspector] public PlayerTowerRotation PlayerTowerRotation;
    [field: HideInInspector] public PlayerGameInfoUI PlayerGameInfoUI;
    [field: HideInInspector] public PlayerMovement PlayerMovement;
    [field: HideInInspector] public PlayerStatus PlayerStatus;
    [field: HideInInspector] public PlayerInputs PlayerInputs;
    [field: HideInInspector] public PlayerShoot PlayerShoot;

    [field: SerializeField] private GameObject m_destroyedTank;

    private void Awake()
    {
        PlayerCameraController = GetComponent<PlayerCameraController>();

        PlayerTowerRotation = GetComponent<PlayerTowerRotation>();

        PlayerGameInfoUI = GetComponent<PlayerGameInfoUI>();

        PlayerMovement = GetComponent<PlayerMovement>();

        PlayerStatus = GetComponent<PlayerStatus>();

        PlayerInputs = GetComponent<PlayerInputs>();

        PlayerShoot = GetComponent<PlayerShoot>();

        LinkChilds(PlayerMovement.MainModel.transform);
    }

    private void LinkChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Collider>() == null)
                continue;

            child.gameObject.AddComponent<LinkToStatus>();

            child.GetComponent<LinkToStatus>().Status = PlayerStatus;

            LinkChilds(child);
        }
    }

    public void DestroyTank()
    {
        Destroy(PlayerMovement.MainModel);

        DiableTank();

        PlayerGameInfoUI.PlayerDefeat();

        GameObject destroyed = Instantiate(m_destroyedTank, PlayerMovement.MainModel.transform.position + Vector3.up, PlayerMovement.MainModel.transform.rotation);

        destroyed.transform.localScale = PlayerMovement.MainModel.transform.localScale;
    }

    public void DiableTank()
    {
        PlayerTowerRotation.enabled = false;
        PlayerMovement.enabled = false;
        PlayerShoot.enabled = false;
    }
}

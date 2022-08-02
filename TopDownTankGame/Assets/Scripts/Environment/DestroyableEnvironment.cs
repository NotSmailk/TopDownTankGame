using UnityEngine;

public class DestroyableEnvironment : MonoBehaviour
{
    [field: SerializeField] private GameObject[] m_destroyedObjects;

    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);

        foreach (GameObject item in m_destroyedObjects)
            Instantiate(item, transform.position, transform.rotation, transform.parent);
    }
}

using UnityEngine;

[System.Serializable]
public class DetectionParametres
{
    [field: Header("Detection settings")]
    [field: SerializeField] public float SphereRadius = 10f;
    [field: SerializeField] public float RadiusAtObservation = 15f;
    [field: SerializeField] public float ViewRadius = 15f;
    [field: SerializeField, Range(0, 360)] public float ViewAngle = 15f;
    [field: SerializeField, Range(0, 360)] public float ShootViewAngle = 15f;

    [field: Header("LayerMasks")]
    [field: SerializeField] public LayerMask DetectionLayer;
    [field: SerializeField] public LayerMask ObstructionLayer;
}

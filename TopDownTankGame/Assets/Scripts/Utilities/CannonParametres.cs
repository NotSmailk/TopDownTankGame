using UnityEngine;

[System.Serializable]
public struct CannonParametres
{
    [field: SerializeField] public Shell Shell;
    [field: SerializeField] public LayerMask TargetMask;
    [field: SerializeField] public int Cooldown;
    [field: SerializeField] public int Damage;
}

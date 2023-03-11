using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Enemy Factory", fileName = "New Enemy Factory")]
public class EnemyFactory : GameObjectFactory
{
    [field: SerializeField] private GameObject _destroyedTank;

    public Enemy Get(EnemyData data)
    {
        var enemy = CreatObject(data.Prefab);
        enemy.OriginFactory = this;
        enemy.Initialize(data.Health, data.Name, data.DetectionParametres, data.CannonParametres);
        return enemy;
    }

    public void Reclaim(Enemy enemy)
    {
        if (_destroyedTank)
            Instantiate(_destroyedTank, enemy.transform.position, enemy.transform.rotation);

        Destroy(enemy.gameObject);
    }
}

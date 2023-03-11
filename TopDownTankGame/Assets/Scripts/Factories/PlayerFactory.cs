using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Player Factory", fileName = "New Player Factory")]
public class PlayerFactory : GameObjectFactory
{
    [field: SerializeField] private GameObject _destroyedTank;

    public Player Get(PlayerData data)
    {
        var player = CreatObject(data.Prefab);
        player.OriginFactory = this;
        player.Initialize(data.Health, data.Parametres);
        return player;
    }

    public void Reclaim(Player player)
    {
        if (_destroyedTank)
            Instantiate(_destroyedTank, player.transform.position, player.transform.rotation);

        Destroy(player.gameObject);
    }
}

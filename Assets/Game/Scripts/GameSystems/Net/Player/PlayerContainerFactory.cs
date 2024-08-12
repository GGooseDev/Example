using Unity.Netcode;
using UnityEngine;

public class PlayerContainerFactory : PrefabInstanceHandlerBase, IService
{
    private NetPlayersContainerHandler _playersContainerHandler;

    public void Init(NetPlayersContainerHandler playersContainerHandler)
    {
        _playersContainerHandler = playersContainerHandler;
    }


    public override void GeneralInitNetObject(GameObject gameObject)
    {
        PlayerContainer playerContainer = gameObject.GetComponent<PlayerContainer>();
        playerContainer.Init(_playersContainerHandler);
    }
}
using Unity.Netcode;
using UnityEngine;

public class SessionData : NetworkBehaviour, IService
{
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private PlayerContainer _playerContainer;
    public NetPlayersContainerHandler PlayerDataHandler;

    public void Init(PlayerContainerFactory factory)
    {
        InitPlayerContainersHandler(factory);
    }

    private void InitPlayerContainersHandler(PlayerContainerFactory playerContainerFactory)
    {
        PlayerDataHandler.Init(_networkManager, _playerContainer, playerContainerFactory);
    }
}
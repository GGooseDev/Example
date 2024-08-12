using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Object = UnityEngine.Object;

public class NetPlayersContainerHandler : NetworkBehaviour, IService
{
    private NetworkManager _networkManager;
    private PlayerContainer _playerContainer;
    private NetworkList<NetworkBehaviourReference> _syncPlayersContainer;
    private List<PlayerContainer> _cashedPlayersContainer = new List<PlayerContainer>();
    private PlayerContainerFactory _factory;
    public IReadOnlyList<PlayerContainer> GetPlayers => _cashedPlayersContainer;
    public event Action<IReadOnlyList<PlayerContainer>> OnPlayersUpdate;


    public void Init(NetworkManager networkManager, PlayerContainer playerContainer,
        PlayerContainerFactory factory)
    {
        _networkManager = networkManager;
        _playerContainer = playerContainer;
        _syncPlayersContainer = new NetworkList<NetworkBehaviourReference>();
        _factory = factory;
        SubscribeToNetEvent();
    }

    public void ForcePlayerDataUpdate()
    {
        OnPlayersUpdate?.Invoke(GetPlayers);
    }

  
    private void SubscribeToNetEvent()
    {
        _syncPlayersContainer.OnListChanged += UpdatePlayerList;
        _networkManager.OnClientConnectedCallback += ClientInitialization;
    }

    private void UpdatePlayerList(NetworkListEvent<NetworkBehaviourReference> changeEvent)
    {
        var tempArray = new PlayerContainer[_syncPlayersContainer.Count];
        var index = 0;
        foreach (var player in _syncPlayersContainer)
        {
            tempArray[index] = (PlayerContainer)player;
            index++;
        }

        _cashedPlayersContainer = tempArray.ToList();
        ForcePlayerDataUpdate();
    }

    private void ClientInitialization(ulong clientId)
    {
        if (_networkManager.IsServer)
        {
            SpawnPlayerContainerObject(clientId);
        }
    }

    private void SpawnPlayerContainerObject(ulong clientId)
    {
        var playerMenu = Instantiate(_playerContainer);
        _factory.GeneralInitNetObject(playerMenu.gameObject);
        playerMenu.NetworkObject.SpawnWithOwnership(clientId);
        DontDestroyOnLoad(playerMenu);
        _syncPlayersContainer.Add(playerMenu);
    }

    public override void OnDestroy()
    {
        base.OnDestroy(); 
        _syncPlayersContainer?.Dispose();
        
    }
}
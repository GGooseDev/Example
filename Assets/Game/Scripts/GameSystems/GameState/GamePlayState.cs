using System;
using Cysharp.Threading.Tasks;
using Unity.Netcode;

public class GamePlayState : IState
{
    private NetworkManager _networkManager;
    private AllServices _services;

    public GamePlayState(GameStateMachine gameStateMachine, AllServices services, NetworkManager networkManager)
    {
        _services = services;
        _networkManager = networkManager;
    }

    public async UniTask Exit()
    {
        return;
    }

    public async UniTask Enter()
    {
        StartPlayerSpawnAndInit();
    }

    private void StartPlayerSpawnAndInit()
    {
        var netPlayerContainersHandler = _services.Single<SessionData>().PlayerDataHandler;
        var playersController = _services.Single<PlayersController>();
        var levelController = _services.Single<LevelController>();
        var submarine = levelController.submarine;
        if (levelController.IsServer)
        {
            playersController.Init(_networkManager);
            playersController.StartPlayersSpawn(netPlayerContainersHandler.GetPlayers, submarine.PlayerSpawnPosition,
                submarine.NetworkObject);
        }
    }
}
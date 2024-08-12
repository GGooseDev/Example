using System;
using Cysharp.Threading.Tasks;
using Unity.Netcode;

public class ConnectionState : IState
{
    private readonly GameStateMachine _stateMachine;
    private const string SCENE_NAME = "UI";
    private readonly AllServices _services;
    private NetworkStarter _networkStarter;
    private NetworkManager _networkManager;

    public ConnectionState(GameStateMachine gameStateMachine, AllServices services, NetworkManager networkManager)
    {
        _networkManager = networkManager;
        _services = services;
        _stateMachine = gameStateMachine;
        _networkStarter = new NetworkStarter(networkManager);
        RegisterServices();
    }

    private void RegisterServices()
    {
        _services.RegisterSingle(_networkStarter);
    }

    public async UniTask Exit()
    {
    }

    public async UniTask Enter()
    {
        _services.Single<SessionData>().Init(RegisterPlayerContainerInstanceHandler());
        _services.Single<NetworkGameStateSwitcher>().Init(_stateMachine);
        await _stateMachine.Enter<LoadSceneState, LoadData>(new LoadData(EnterMenu, false, SCENE_NAME) { });
    }

    private PlayerContainerFactory RegisterPlayerContainerInstanceHandler()
    {
        var playerContainerInstanceHandler = _services.Single<PlayerContainerFactory>();
        playerContainerInstanceHandler.Init(_services.Single<SessionData>().PlayerDataHandler);
        playerContainerInstanceHandler.Registration(_networkManager);
        return playerContainerInstanceHandler;
    }

    private async UniTask EnterMenu()
    {
        await _stateMachine.Enter<MenuInitState>();
    }
}
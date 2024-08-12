using Cysharp.Threading.Tasks;
using Unity.Netcode;

public class MenuInitState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;
    private readonly NetworkManager _networkManager;
    private const string SCENE_NAME = "GameScene"; //todo переделать и  прокидывать из sessiondata

    public MenuInitState(GameStateMachine gameStateMachine, AllServices services, NetworkManager networkManager)
    {
        _stateMachine = gameStateMachine;
        _services = services;
        _networkManager = networkManager;
    }

    public async UniTask Exit()
    {
    }


    public async UniTask Enter()
    {
        var uiController = _services.Single<UIController>();
        var networkStarter = _services.Single<NetworkStarter>();
        var menuData = CreateInitData(networkStarter);
        uiController.Init(menuData);
    }

    private MenuInitData CreateInitData(NetworkStarter networkStarter)
    {
        return new MenuInitData
        (
            _networkManager,
            () => networkStarter.StartHost(),
            () => networkStarter.StartClient(),
            LoadGameplayScene,
            _services.Single<SessionData>().PlayerDataHandler
        );
    }

    private void LoadGameplayScene()
    {
        var networkGameStateSwitcher = AllServices.Container.Single<NetworkGameStateSwitcher>();
        networkGameStateSwitcher.LoadGamePlayRpc(SCENE_NAME);
    }
}
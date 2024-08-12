using Cysharp.Threading.Tasks;
using Unity.Netcode;

public class LoadSceneState : IPayloadState<LoadData>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly AllServices _allServices;
    private readonly SceneLoader _sceneLoader;

    public LoadSceneState(GameStateMachine gameStateMachine, AllServices allServices, NetworkManager networkManager)
    {
        _gameStateMachine = gameStateMachine;
        _allServices = allServices;
        _sceneLoader = new SceneLoader(networkManager);
    }

    public async UniTask Enter(LoadData data)
    {
        await _sceneLoader.Load(data);
    }

    public async UniTask Exit()
    {
        return;
    }
}
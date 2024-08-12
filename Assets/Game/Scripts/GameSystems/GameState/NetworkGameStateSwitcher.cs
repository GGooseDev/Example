using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;


public class NetworkGameStateSwitcher : NetworkBehaviour, IService
{
    private GameStateMachine _stateMachine;

    public void Init(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    [Rpc(SendTo.Server)]
    public void LoadGamePlayRpc(FixedString32Bytes sceneName)
    {
        ChangeToGameplayStateRpc(sceneName);
    }

    [Rpc(SendTo.Everyone)]
    private void ChangeToGameplayStateRpc(FixedString32Bytes sceneName)
    {
        _stateMachine.Enter<LoadSceneState, LoadData>(new LoadData(ChangeStateToGamePlay, true, sceneName.Value) { })
            .Forget();
    }

    private async UniTask ChangeStateToGamePlay()
    {
        await _stateMachine.Enter<GamePlayState>();
    }
}
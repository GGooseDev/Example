using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Netcode;


public class GameStateMachine : IService
{
    private readonly Dictionary<Type, IBaseState> _states;
    private IBaseState _activeState;


    public GameStateMachine(AllServices services, NetworkManager networkManager)
    {
        _states = new Dictionary<Type, IBaseState>
        {
            [typeof(LoadSceneState)] = new LoadSceneState(this, services, networkManager),
            [typeof(ConnectionState)] = new ConnectionState(this, services, networkManager),
            [typeof(MenuInitState)] = new MenuInitState(this, services, networkManager),
            [typeof(GamePlayState)] = new GamePlayState(this, services,networkManager)
        };
    }

    public async UniTask Enter<T>() where T : class, IState
    {
        var state = await ChangeState<T>();
        await state.Enter();
    }

    private async UniTask<T> ChangeState<T>() where T : class, IBaseState
    {
        if (_activeState != null)
            await _activeState.Exit();
        T state = GetState<T>();
        _activeState = state;
        return state;
    }

    public T GetState<T>() where T : class, IBaseState
    {
        T state = _states[typeof(T)] as T;
        return state;
    }

    public async UniTask Enter<T, TPayload>(TPayload TParameter) where T : class, IPayloadState<TPayload>
    {
        T state = await ChangeState<T>();
        await state.Enter(TParameter);
    }
}
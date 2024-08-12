using Cysharp.Threading.Tasks;

public interface IState : IBaseState
{
    public UniTask Enter();
}

public interface IPayloadState<TParameter> : IBaseState
{
    public UniTask Enter(TParameter data);
}


public interface IBaseState
{
    public UniTask Exit();
}
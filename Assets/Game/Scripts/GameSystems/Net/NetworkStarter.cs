using System;
using Unity.Netcode;

public class NetworkStarter : IService
{
    private readonly NetworkManager _manager;

    public NetworkStarter(NetworkManager manager)
    {
        _manager = manager;
    }

    public void StartHost(Action postAction=null)
    {
        if (_manager.StartHost())
        {
            postAction?.Invoke();
        }
    }

    public void StartClient(Action postAction=null)
    {
        if (_manager.StartClient()) //только для тестов на 1 пк
        {
            postAction?.Invoke();
            return;
        }

        _manager.Shutdown();
    }
}
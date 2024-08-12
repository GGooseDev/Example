using UnityEngine;
using UnityEngine.Serialization;

public class BootInstaller : Installer
{
    [SerializeField] private SessionData _sessionData;
    [SerializeField] private NetworkGameStateSwitcher _networkStateSwitcher;
    [SerializeField] private NetPlayersContainerHandler _netPlayersContainerHandler;
    [SerializeField] private PlayerContainerFactory _playersContainerFactory;

    public override void Install(AllServices allServices)
    {
        allServices.RegisterSingle(_sessionData);
        allServices.RegisterSingle(_networkStateSwitcher);
        allServices.RegisterSingle(_netPlayersContainerHandler);
        allServices.RegisterSingle(_playersContainerFactory);
    }
}
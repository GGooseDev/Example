using System;
using Unity.Netcode;

public class MenuInitData
{
    public MenuInitData(NetworkManager networkManager, Action startServer, Action startClient, Action startGame, NetPlayersContainerHandler playersContainersHandler)
    {
        NetworkManager = networkManager;
        StartServer = startServer;
        StartClient = startClient;
        StartGame = startGame;
        PlayersContainersHandler = playersContainersHandler;
    }

    public NetworkManager NetworkManager { get; private set; }
    public Action StartServer { get; private set; }
    public Action StartClient { get; private set; }
    public Action StartGame { get; private set; }
    public NetPlayersContainerHandler PlayersContainersHandler{ get; private set; }
}
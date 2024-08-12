using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour, IService
{
    [SerializeField] private UIViewChanger _viewChanger;
    [SerializeField] private TestStartServerClientView _testStartView;
    [SerializeField] private AwaitingView _awaitingView;

    public void Init(MenuInitData menuInitData)
    {
        _testStartView.Init(menuInitData.StartServer, menuInitData.StartClient);
        _awaitingView.Init(menuInitData.NetworkManager, menuInitData.StartGame, menuInitData.PlayersContainersHandler);
    }
}
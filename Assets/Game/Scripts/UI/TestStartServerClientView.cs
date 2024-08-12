using System;
using UnityEngine;
using UnityEngine.UI;

public class TestStartServerClientView:View
{
    [SerializeField] private Button _startServerButton;
    [SerializeField] private Button _startClientButton;

    public void Init(Action startServer, Action startClient)
    {
        _startServerButton.onClick.AddListener(startServer.Invoke);
        _startClientButton.onClick.AddListener(startClient.Invoke);
        _startServerButton.onClick.AddListener(ShowAwaitView);
        _startClientButton.onClick.AddListener(ShowAwaitView);
    }

    private void ShowAwaitView()
    {
        _viewChanger.ShowView<AwaitingView>();
    }
    
}
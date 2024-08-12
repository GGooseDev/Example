using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class AwaitingView : View
{
    [SerializeField] private Button _startGame;
    [SerializeField] private UIMenuPlayerBlock[] _playersList;
    private NetPlayersContainerHandler _containersHandler;
    private NetworkManager _netManager;

    public void Init(NetworkManager manager, Action startGame, NetPlayersContainerHandler netPlayersContainerHandler)
    {
        _netManager = manager;
        _containersHandler = netPlayersContainerHandler;
        _startGame.onClick.AddListener(startGame.Invoke);
        _containersHandler.OnPlayersUpdate += UpdateUIPlayerList;
    }

    public override void Show()
    {
        base.Show();
        _startGame.gameObject.SetActive(_netManager.IsServer);
    }

    private void OnDestroy()
    {
        _containersHandler.OnPlayersUpdate -= UpdateUIPlayerList;
    }

    private void UpdateUIPlayerList(IReadOnlyList<PlayerContainer> players)
    {
        for (int i = 0; i < _playersList.Length; i++)
        {
            if (i >= players.Count)
            {
                _playersList[i].gameObject.SetActive(false);
            }
            else
            {
                _playersList[i].SetData(players[i].Nick.Value.ToString());
                _playersList[i].gameObject.SetActive(true);
            }
        }
    }
}
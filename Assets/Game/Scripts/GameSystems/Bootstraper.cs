using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class Boostraper : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var gameStateMachine = new GameStateMachine(new AllServices(), _networkManager);
        gameStateMachine.Enter<ConnectionState>().Forget();
    }
}
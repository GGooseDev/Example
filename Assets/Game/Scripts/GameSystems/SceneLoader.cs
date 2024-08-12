using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneLoader
{
    private AsyncOperation _asyncLoad;
    private NetworkManager _networkManager;
    private NetworkSceneManager _networkSceneManager;
    private bool _isLocalLoaded;

    public SceneLoader(NetworkManager networkManager)
    {
        _networkManager = networkManager;
    }


    public async UniTask Load(LoadData loadData)
    {
        await CheckAndLoad(loadData);
    }

    private async UniTask CheckAndLoad(LoadData loadData)
    {
        try
        {
            await LoadProcess(loadData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async UniTask LoadProcess(LoadData loadData)
    {
        if (loadData.IsNetLoading)
        {
            await LoadNetSceneForAllPlayers(loadData);
        }
        else
        {
            await LoadSingleScene(loadData);
        }

        await loadData.LoadedAction.Invoke();
    }


    private NetworkSceneManager CheckExistNetworkSceneManagerAndGet()
    {
        if (_networkManager.SceneManager == null)
        {
            throw new Exception("no found scene manager");
        }

        return _networkManager.SceneManager;
    }


    private async UniTask LoadNetSceneForAllPlayers(LoadData loadData)
    {
        CheckExistNetworkSceneManagerAndGet().OnLoadEventCompleted += Loaded;
        if (NetworkManager.Singleton.IsServer)
        {
            ServerLoadScene(loadData);
        }

        await UniTask.WaitWhile(() => !_isLocalLoaded);
        await UniTask.NextFrame();
        Debug.Log("loaded");
        _isLocalLoaded = false;
    }

    private async UniTask LoadSingleScene(LoadData loadData)
    {
        await SceneManager.LoadSceneAsync(loadData.SceneName);
    }

    private void ServerLoadScene(LoadData loadData)
    {
        CheckExistNetworkSceneManagerAndGet().LoadScene(loadData.SceneName, LoadSceneMode.Single);
    }


    private void Loaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted,
        List<ulong> clientsTimedOut)
    {
        CheckExistNetworkSceneManagerAndGet().OnLoadEventCompleted -= Loaded;
        _isLocalLoaded = true;
    }
}
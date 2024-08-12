using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayersController : NetworkBehaviour, IService
{
    [FormerlySerializedAs("_playerPref")] [SerializeField]
    private PlayerCharacter playerCharacterPref;

    public List<PlayerCharacter> Players = new List<PlayerCharacter>();

    public void Init(NetworkManager networkManager)
    {
        networkManager.OnClientDisconnectCallback += DeletePlayer;
    }

    private void DeletePlayer(ulong id)
    {
        Players.Remove(Players.First(i => i.OwnerClientId == id));
    }

    public void StartPlayersSpawn(IEnumerable<PlayerContainer> playersData, Vector3[] spawnPos, NetworkObject parent)
    {
       
        foreach (var player in playersData)
        {
            Players.Add(InstantiatePlayer(player));
        }

        AwaitTest(parent,spawnPos).Forget();//todo после доделки синка спавна с ожиданием будет не нужен
    }

    private async UniTask AwaitTest(NetworkObject networkObject, Vector3[] spawnPos)
    {   List<Vector3> cashedList = spawnPos.ToList();
        await UniTask.Delay(200);
        foreach (var player in Players)
        {
            player.GetComponent<PlayerMovement>().SyncPosRpc(GetRandomPlayerPos(cashedList ));
            player.NetworkObject.TrySetParent(networkObject);
        }
    }
  
    private PlayerCharacter InstantiatePlayer(PlayerContainer playersData)
    {
        PlayerCharacter playerCharacter = Instantiate(playerCharacterPref, Vector3.zero,Quaternion.identity);
        playerCharacter.NetworkObject.SpawnAsPlayerObject(playersData.OwnerClientId, true);
        return playerCharacter;
    }

    private Vector3 GetRandomPlayerPos(List<Vector3> allSpawnPos)
    {
        var randomSpawnPos = allSpawnPos[Random.Range(0, allSpawnPos.Count)];
        allSpawnPos.Remove(randomSpawnPos);
        return randomSpawnPos;
    }
}
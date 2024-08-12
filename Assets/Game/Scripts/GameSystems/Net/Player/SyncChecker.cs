using System;
using Unity.Netcode;

public class SyncChecker : NetworkBehaviour
{
    private int _clientsCount;
    private int _clientsSpawnedCount;
    public event Action AllSpawned;

    public void Init(int clientsCount)
    {
        _clientsCount = clientsCount;
    }
    public override void OnNetworkSpawn()
    {
        CheckAllClientsSpawn();
    }

    private void CheckAllClientsSpawn()
    {
        _clientsCount++;
        if (_clientsCount == _clientsSpawnedCount)
        {
            SyncAllSpawnedEventRpc();
        }
    }

    [Rpc(SendTo.Everyone)]
    private void SyncAllSpawnedEventRpc()
    {
        AllSpawned?.Invoke();
    }
}
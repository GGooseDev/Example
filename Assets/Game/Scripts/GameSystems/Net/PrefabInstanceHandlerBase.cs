using Unity.Netcode;
using UnityEngine;


public abstract class PrefabInstanceHandlerBase : MonoBehaviour, INetworkPrefabInstanceHandler
{
    public GameObject prefab;

    public void Registration(NetworkManager networkManager)
    {
        networkManager.PrefabHandler.AddHandler(prefab,this);
    }

    public virtual NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        var container = Object.Instantiate(prefab);
        GeneralInitNetObject(container);
        return container.GetComponent<NetworkObject>();
    }

    public abstract void GeneralInitNetObject(GameObject gameObject);

    public virtual void Destroy(NetworkObject networkObject)
    {
        Object.Destroy(networkObject);
    }
}
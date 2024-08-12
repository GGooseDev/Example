using Unity.Collections;
using Unity.Netcode;

public class PlayerContainer : NetworkBehaviour
{
    private NetPlayersContainerHandler _playersContainerHandler;

    public NetworkVariable<FixedString32Bytes> Nick =
        new NetworkVariable<FixedString32Bytes>(default, NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

    public void Init(NetPlayersContainerHandler playersContainerHandler)
    {
        _playersContainerHandler = playersContainerHandler;
        Nick.OnValueChanged += OnNickChanged;
    }

    private void OnNickChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
    {
        _playersContainerHandler?.ForcePlayerDataUpdate();
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
            SetDebugNick();
    }

    private void SetDebugNick()
    {
        Nick.Value = System.Environment.MachineName;
    }
}
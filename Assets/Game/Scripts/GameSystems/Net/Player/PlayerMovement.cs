using Unity.Netcode;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private IPlayerControl _playerControl;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private int lookYLimit;
    [SerializeField] private Transform _playerCamera;

    private NetworkVariable<Quaternion> _syncRotation = new NetworkVariable<Quaternion>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<Vector2> _syncPos = new NetworkVariable<Vector2>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private float _rotationY;
    private float _rotationX;

    public override void OnNetworkSpawn()
    {
        InitControl();
    }

    [Rpc(SendTo.Everyone)]
    public void SyncPosRpc(Vector3 pos)
    {
        transform.position = pos;
        if (IsLocalPlayer)
        {
            //  _characterController.enabled = true;
        }
    }

    private void InitControl()
    {
        if (IsLocalPlayer)
        {
            _playerControl = GetComponent<IPlayerControl>();
        }
        else
        {
            _rb.isKinematic = true;
        }
    }


    private void Update()
    {
        OwnerRotationUpdate();
        NoOwnerRecoveryRotation();
    }

    private void FixedUpdate()
    {
        ServerMoving();
        NoOwnerRecoveryMoving();
    }


    private void ServerMoving()
    {
        if (IsLocalPlayer)
        {
            var gravity = Vector3.down * 10;
            Vector3 forward = transform.forward * _playerControl.GetVerticalAxis;
            Vector3 right = transform.right * _playerControl.GetHorizontalAxis;
            Vector3 moveDirection = (forward + right).normalized * _moveSpeed;

            _rb.velocity = (moveDirection);
            _syncPos.Value = transform.localPosition;
        }
    }

    private void OwnerRotationUpdate()
    {
        if (IsLocalPlayer)
        {
            _rotationY -= _playerControl.GetVerticalLookAxis * _lookSpeed;
            _rotationY = Mathf.Clamp(_rotationY, -lookYLimit, lookYLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);
            transform.rotation *= Quaternion.Euler(0, _playerControl.GetHorizontalLookAxis * _lookSpeed, 0);
            _syncRotation.Value = transform.rotation;
        }
    }

    private void NoOwnerRecoveryRotation()
    {
        if (!IsLocalPlayer)
        {
            transform.rotation = _syncRotation.Value;
        }
    }

    private void NoOwnerRecoveryMoving()
    {
        if (!IsLocalPlayer)
        {
            transform.localPosition = _syncPos.Value;
        }
    }
}
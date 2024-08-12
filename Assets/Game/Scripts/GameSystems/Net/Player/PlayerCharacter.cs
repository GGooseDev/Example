using Unity.Netcode;
using UnityEngine;


public class PlayerCharacter : NetworkBehaviour
{
    [SerializeField] private Camera _fpvCamera;
  
    public override void OnNetworkSpawn()
    {
        CheckCameraAndActivate();
    }
    

    private void CheckCameraAndActivate()
    {
        if (IsLocalPlayer)
        {
            _fpvCamera.enabled = true;
        }
    }

}
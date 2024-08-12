using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class Submarine : NetworkBehaviour
{
    [SerializeField] private Transform[] _playerSpawnPosition;
    public Vector3[] PlayerSpawnPosition => _playerSpawnPosition.Select(i => i.position).ToArray();

  
}
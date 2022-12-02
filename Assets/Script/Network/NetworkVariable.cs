using Unity.Netcode;
using UnityEngine;

public class NetworkVariable : NetworkBehaviour
{
    private NetworkVariable<Vector3> SpawnPosition = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnPosition.Value = new Vector3(0, 0, 0);
            Debug.Log("Dit is de server");
        }

        if (IsHost)
        {
            SpawnPosition.Value = new Vector3(-1, 0, 0);
            Debug.Log("Dit is de host");
        }
        if (IsClient)
        {
            SpawnPosition.Value = new Vector3(-1, 0, 0);
            Debug.Log("Dit is de client");
        }
    }
}

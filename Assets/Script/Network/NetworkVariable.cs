using Unity.Netcode;
using UnityEngine;

public class NetworkVariable : NetworkBehaviour
{
    private NetworkVariable<Vector3> SpawnPosition = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Dit is de server");
        }
        if (IsHost)
        {
            Debug.Log("Dit is de host");
        }
        if (IsClient)
        {
            SpawnPosition = new NetworkVariable<Vector3>(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z));
            
            Debug.Log("Dit is de client: " + SpawnPosition);
        }
    }
}
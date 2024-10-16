using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionConfigurer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = $"Player{Random.Range(0, 999)}";
        PhotonNetwork.GameVersion = "0.0.0"; // locks users to a specific version
        PhotonNetwork.ConnectUsingSettings(); // Connect to the server

        Debug.Log($":: Connect to the server using the provided settings");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($":: OnConnectedToMaster");

        if (!PhotonNetwork.InLobby && !PhotonNetwork.IsMasterClient)
        {
            Debug.Log($":: !PhotonNetwork.InLobby :: JoiningLobby");
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        Debug.Log($":: OnDisconnected due to {cause.ToString()}");
    }
}

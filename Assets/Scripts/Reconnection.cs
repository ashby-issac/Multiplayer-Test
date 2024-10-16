using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Reconnection : MonoBehaviourPunCallbacks, IConnectionCallbacks
{
    [SerializeField] private SettingsSO settingsSO;

    private LoadBalancingClient loadBalancingClient;
    private AppSettings appSettings;

    public bool shouldReconnect = true;
    bool reconnecting = false;

    void Start()
    {
        loadBalancingClient = PhotonNetwork.NetworkingClient;
        appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        loadBalancingClient.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        if (loadBalancingClient == null)
            return;

        loadBalancingClient.RemoveCallbackTarget(this);
    }

    void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
    {
        if (!shouldReconnect) // double check in different cases
            return;

        if (this.CanRecoverFromDisconnect(cause))
        {
            Debug.Log($":: Attempting post-disconnection recovery");
            this.Recover();
        }
        else
        {
            Debug.Log($":: Could not attempt recovery from disconnection");
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
        }
        return false;
    }

    private void Recover()
    {
        if (!loadBalancingClient.ReconnectAndRejoin())
        {
            Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
            if (!loadBalancingClient.ReconnectToMaster())
            {
                Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                if (!loadBalancingClient.ConnectUsingSettings(appSettings))
                {
                    Debug.LogError("ConnectUsingSettings failed");
                }
                else
                {
                    Debug.LogError("ConnectUsingSettings Successful");
                }
            }
        }
        else
        {
            Debug.Log($"ReconnectAdndRejoin (Master Server) Success");
            reconnecting = true;
            settingsSO.isReconnecting = true;
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (reconnecting)
            Debug.LogError($"Reconnection - OnJoinRoomFailed: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (reconnecting)
            Debug.LogError($"Reconnection - OnJoinRandomFailed: " + message);
    }

    public override void OnJoinedRoom()
    {
        Invoke(nameof(DisableReconnecting), 3f);
    }

    void DisableReconnecting()
    {
        settingsSO.isReconnecting = false; 
        Debug.Log($" Reconnection: DisableReconnecting ");
    }

    #region Unused Methods

    void IConnectionCallbacks.OnConnected()
    {
        Debug.Log($" Reconnection: OnConnected ");
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
        Debug.Log($" Reconnection: OnConnectedToMaster ");
    }

    void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    #endregion
}
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private Button createRoomBtn;

    private RoomCanvases roomCanvases;

    private void OnEnable()
    {
        createRoomBtn.onClick.AddListener(OnCreateRoomClicked);
    }

    private void OnDisable()
    {
        createRoomBtn.onClick.RemoveListener(OnCreateRoomClicked);
    }

    public void Initialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
    }

    public void OnCreateRoomClicked()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        options.CleanupCacheOnLeave = false; // Do not clean the props that the player set
        // time to live -> how long the server can go without receiving an update from a player before the player is completely removed
        options.PlayerTtl = -1; // no timeout: the player stays in the room until the lifetime of the room ends
        options.EmptyRoomTtl = 300000; // time limit of 5 min (which is the max limit) 

        PhotonNetwork.KeepAliveInBackground = SettingsSO.timeout;

        PhotonNetwork.JoinOrCreateRoom($"{roomName.text}", options, TypedLobby.Default);
    }
}

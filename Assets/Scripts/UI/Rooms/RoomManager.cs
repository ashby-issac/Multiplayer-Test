using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private RoomCanvases roomCanvases;

    public static RoomManager Instance;

    private void Awake()
    {
        roomCanvases = roomCanvases ?? FindObjectOfType<RoomCanvases>();

        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
                roomCanvases.RoomCreationCanvas.RoomListingMenu.RemoveRemoveFromList(room);
            else
                roomCanvases.RoomCreationCanvas.RoomListingMenu.AddNewRoom(room);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($":: OnConnectedToMaster");

        roomCanvases.RoomCreationCanvas.CreateRoomMenu.ShowConnectedMsg();
        if (!PhotonNetwork.InLobby && !PhotonNetwork.IsMasterClient)
        {
            Debug.Log($":: !PhotonNetwork.InLobby :: JoiningLobby");
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Created Room Successfully");
        PhotonNetwork.JoinLobby();

        roomCanvases.CurrentRoomCanvas.Show(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Creating Room Failed");
    }

    public override void OnJoinedRoom()
    {
        roomCanvases.CurrentRoomCanvas?.Show(true);
        roomCanvases.RoomCreationCanvas?.RoomListingMenu.ResetRoomsList();
    }

    // Remote Player entering and leaving
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        roomCanvases.CurrentRoomCanvas.PlayerListingMenu.AddPlayerToRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomCanvases.CurrentRoomCanvas.PlayerListingMenu.ResetPlayersList(otherPlayer);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);

        Debug.Log($":: OnMasterClientSwitched");
        //roomCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }
}

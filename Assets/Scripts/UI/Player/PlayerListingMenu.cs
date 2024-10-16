using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private PlayerListInfo playerListInfo;
    [SerializeField] private TextMeshProUGUI readyText;

    private List<PlayerListInfo> playersList = new List<PlayerListInfo>();
    private RoomCanvases roomCanvases;

    private bool isReady = false;

    public void Initialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
    }

    public void ResetPlayersList(Player otherPlayer)
    {
        var index = playersList.FindIndex(playerInfo => playerInfo.player == otherPlayer);
        if (index == -1) return;

        Destroy(playersList[index].gameObject);
        playersList.RemoveAt(index);
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i=0; i<playersList.Count; i++)
            {
                if (PhotonNetwork.LocalPlayer == playersList[i].player) continue;

                if (!playersList[i].IsReady) return;
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void OnClick_ReadyState()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyState(!isReady);
            photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, isReady);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool isReady)
    {
        var index = playersList.FindIndex(p => p.player == player);
        if (index != -1)
            playersList[index].IsReady = isReady;
    }

    public override void OnEnable()
    {
        SetReadyState(false);
        GetCurrentRoomPlayers();
    }

    private void SetReadyState(bool state)
    {
        isReady = state;
        readyText.text = state ? "R" : "N";
    }

    public override void OnDisable()
    {
        for (int i = 0; i < playersList.Count; i++)
            Destroy(playersList[i].gameObject);

        playersList.Clear();
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;

        foreach (var playerDict in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerToRoom(playerDict.Value);
        }
    }

    public void AddPlayerToRoom(Player newPlayer)
    {
        var index = playersList.FindIndex(p => p.player.NickName == newPlayer.NickName);
        if (index == -1)
        {
            var _playerListInfo = Instantiate(playerListInfo, content);
            _playerListInfo.SetPlayerName(newPlayer);
            playersList.Add(_playerListInfo);
        }
        else
        {
            playersList[index].SetPlayerName(newPlayer);
        }
    }
}

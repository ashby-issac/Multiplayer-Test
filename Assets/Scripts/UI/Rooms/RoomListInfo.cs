using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;

    private RoomInfo roominfo;
    public string RoomName => roomName.text;

    public void SetRoomName(RoomInfo _roomInfo)
    {
        roominfo = _roomInfo;
        roomName.text = _roomInfo.Name;
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
}

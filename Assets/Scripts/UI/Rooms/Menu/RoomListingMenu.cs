using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : MonoBehaviour
{
    [SerializeField] private RoomListInfo roomListInfo;
    [SerializeField] private Transform content;

    private List<RoomListInfo> roomsList = new List<RoomListInfo>();
    private RoomCanvases roomCanvases;

    public void Initialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
    }

    public void AddNewRoom(RoomInfo room)
    {
        var index = roomsList.FindIndex(info => info.RoomName == room.Name);
        if (index == -1)
        {
            var _roomListInfo = Instantiate(roomListInfo, content);
            _roomListInfo.SetRoomName(room);
            roomsList.Add(_roomListInfo);
        }
    }

    public void RemoveRemoveFromList(RoomInfo room)
    {
        var index = roomsList.FindIndex(info => info.RoomName == room.Name);
        if (index != -1)
        {
            Destroy(roomsList[index].gameObject);
            roomsList.RemoveAt(index);
        }
    }

    public void ResetRoomsList()
    {
        content.DestroyChildren(true);
        roomsList.Clear();
    }
}

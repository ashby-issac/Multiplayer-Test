using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private PlayerListingMenu playerListingMenu;
    [SerializeField] private LeaveRoomMenu leaveRoomMenu;

    public PlayerListingMenu PlayerListingMenu => playerListingMenu;
    public LeaveRoomMenu LeaveRoomMenu => leaveRoomMenu;

    private RoomCanvases roomCanvases;

    public void Intialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
        playerListingMenu.Initialize(roomCanvases);
        leaveRoomMenu.Initialize(roomCanvases);
    }

    public void Show(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

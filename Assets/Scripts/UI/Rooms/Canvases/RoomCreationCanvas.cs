using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreationCanvas : MonoBehaviour
{
    [SerializeField] private CreateRoomMenu createRoomMenu;
    [SerializeField] private RoomListingMenu roomListingMenu;

    private RoomCanvases roomCanvases;

    public CreateRoomMenu CreateRoomMenu => createRoomMenu;
    public RoomListingMenu RoomListingMenu => roomListingMenu;

    public void Intialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
        createRoomMenu.Initialize(roomCanvases);
        roomListingMenu.Initialize(roomCanvases);
    }

    public void Show(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

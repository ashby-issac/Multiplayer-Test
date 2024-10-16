using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    private RoomCanvases roomCanvases;

    public void StartGame()
    {
        roomCanvases.RoomCreationCanvas.Show(true);
    }

    public void Initialize(RoomCanvases _roomCanvases)
    {
        roomCanvases = _roomCanvases;
    }
}

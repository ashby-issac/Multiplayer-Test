using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField] private MenuCanvas menuCanvas;
    [SerializeField] private RoomCreationCanvas roomCreationCanvas;
    [SerializeField] private CurrentRoomCanvas currentRoomCanvas;

    public RoomCreationCanvas RoomCreationCanvas => roomCreationCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas => currentRoomCanvas;

    private void Awake()
    {
        InitializeCanvases();
    }

    private void Start()
    {
        RoomManager.Instance?.Initialize(this);
    }

    private void InitializeCanvases()
    {
        menuCanvas.Initialize(this);
        roomCreationCanvas.Intialize(this);
        currentRoomCanvas.Intialize(this);
    }
}

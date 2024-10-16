using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private ScoresListingMenu scoresMenu;

    private PhotonView photonView;
    private UIManager uiManager;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        pauseBtn.onClick.AddListener(() => OnClick_Pause());
    }

    public void Initialize(UIManager _uiManager)
    {
        uiManager = _uiManager;
    }

    public void OnClick_Pause()
    {
        photonView.RPC("RPC_PauseSim", RpcTarget.AllBuffered, true);
    }

    public void OnClick_Resume()
    {
        photonView.RPC("RPC_PauseSim", RpcTarget.AllBuffered, false);
    }

    [PunRPC]
    void RPC_PauseSim(bool menuState)
    {
        UIManager.isSimulating = menuState;
        scoresMenu.gameObject.SetActive(menuState);
    }

}

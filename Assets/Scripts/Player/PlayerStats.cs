using Photon.Pun;
using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //ExitGames.Client.Photon.Hashtable CustomProps = new ExitGames.Client.Photon.Hashtable();

    private int score;
    private PhotonView photonView;

    UIManager uiManager;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void Initialize(UIManager _uiManager)
    {
        uiManager = _uiManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Collectable"))
            return;

        if (photonView.IsMine)
        {
            score++;
            UIManager.OnScoreChange(score);

            ExitGames.Client.Photon.Hashtable CustomProps = new ExitGames.Client.Photon.Hashtable();
            CustomProps["Score"] = score;
            PhotonNetwork.LocalPlayer.SetCustomProperties(CustomProps);

            EventManager.TriggerCollectiblePicked(PhotonNetwork.LocalPlayer.UserId);
        }

    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;

    public Player Player => PhotonNetwork.LocalPlayer;
    public string PlayerName => playerName.text;

    public void SetPlayerScoreInfo(string name, int score)
    {
        playerName.text = name;
        playerScore.text = $"{score}";
    }
}

using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;

    public bool IsReady;
    public Player player { get; private set; }

    public void SetPlayerName(Player _player)
    {
        player = _player;
        playerName.text = $"{player.NickName}";
    }
}

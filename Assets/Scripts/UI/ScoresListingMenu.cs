using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresListingMenu : MonoBehaviour
{
    [SerializeField] private PlayerScoreInfo scoreListInfo;
    [SerializeField] private Transform content;
    [SerializeField] private Button mainMenuBtn;

    private List<PlayerScoreInfo> playersList = new List<PlayerScoreInfo>();


    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() => GoToMainMenu());
    }

    private void OnEnable()
    {
        UpdatePlayersScoreList();
    }

    private void UpdatePlayersScoreList()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($":: player.CustomProperties[Score]: {player.CustomProperties["Score"]}");

            int index = playersList.Count == 0 ? -1 : playersList.FindIndex(p => p.PlayerName == player.NickName);
            if (index == -1)
            {
                AddToPlayersList(player);
            }
            else
            {
                int val = player.CustomProperties.ContainsKey("Score") ? (int)player.CustomProperties["Score"] : 0;
                playersList[index].SetPlayerScoreInfo(player.NickName, val);
            }
        }
    }

    private void OnDestroy()
    {
        mainMenuBtn.onClick.RemoveListener(() => GoToMainMenu());
    }

    private void GoToMainMenu()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void AddToPlayersList(Player player)
    {
        int val = player.CustomProperties.ContainsKey("Score") ? (int)player.CustomProperties["Score"] : 0;
        var instance = Instantiate(scoreListInfo, content);
        instance.SetPlayerScoreInfo(player.NickName, val);
        playersList.Add(instance);
    }

    public void RemoveFromPlayersList(Player player)
    {
        int index = playersList.Count == 0 ? -1 : playersList.FindIndex(p => p.PlayerName == player.NickName);
        Destroy(playersList[index]);
        playersList.RemoveAt(index);
    }
}

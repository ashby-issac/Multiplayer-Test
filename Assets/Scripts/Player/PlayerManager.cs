using Photon.Pun;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    PhotonView photonView;
    DateTime startTime;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        startTime = DateTime.Now;
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            CreatePlayerController();
        }
    }

    private void CreatePlayerController()
    {
        Debug.Log($" Instantiate Player Controller ");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), 
                                    new Vector3(UnityEngine.Random.Range(-10, 10), 1f, UnityEngine.Random.Range(-10, 10)), 
                                    Quaternion.identity);
    }

    private void OnApplicationQuit()
    {
        Debug.Log($"PlayerManager :: OnApplicationQuit :: TriggerLevelCompleted");
        EventManager.TriggerLevelCompleted(SceneManager.sceneCountInBuildSettings, PhotonNetwork.LocalPlayer.UserId, startTime.ToString(), DateTime.Now.ToString());
    }
}

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int collectablesSpawnCount;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        SpawnCollectables();
    }

    private void SpawnCollectables()
    {
        for (int i = 0; i < collectablesSpawnCount; i++)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Collectable"), new Vector3(UnityEngine.Random.Range(-10, 10), 1f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.Euler(new Vector3(-90f, 0, 0)));
    }
}

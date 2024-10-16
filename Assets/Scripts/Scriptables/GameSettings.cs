using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    [SerializeField] private string nickName = "Player";

    public string GameVersion => gameVersion;
    public string NickName => $"{nickName}{Random.Range(0, 9999)}";
}

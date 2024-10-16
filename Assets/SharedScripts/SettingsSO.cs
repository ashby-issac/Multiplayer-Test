using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/SettingsSO")]
public class SettingsSO : SingletonScriptableObject<SettingsSO>
{
    [Header(" Photon ")]
    public bool dontDestroyOnLeave = true;
    public bool isReconnecting = false;

    [HideInInspector] float disconnectTimeoutMS = 60f * 1000f;
    public static float timeout
    {
        get
        {
            return SettingsSO.Instance.disconnectTimeoutMS;
        }
    }
}

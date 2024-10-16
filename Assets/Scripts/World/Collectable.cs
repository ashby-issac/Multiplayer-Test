using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
            photonView.RPC("RPC_CollectItem", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_CollectItem()
    {
        Destroy(gameObject);
    }
}

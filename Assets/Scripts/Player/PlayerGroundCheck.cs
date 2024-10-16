using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerController playerController;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject || other.gameObject.tag == "Player")
            return;

        IsGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject || other.gameObject.tag == "Player")
            return;

        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject || other.gameObject.tag == "Player")
            return;

        IsGrounded = false;
    }
}

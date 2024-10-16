using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDuration = 0.06f;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private Transform point;

    private Rigidbody playerRb;
    private PhotonView photonView;
    private InputManager inputManager;
    private CapsuleCollider collider;
    private PlayerGroundCheck playerGroundCheck;

    private bool isJumping;

    Vector3 velGravity;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        inputManager = GetComponent<InputManager>();
        collider = GetComponent<CapsuleCollider>();
        playerGroundCheck = GetComponentInChildren<PlayerGroundCheck>();

        velGravity = new Vector3(0, -Physics.gravity.y, 0);
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine || !inputManager || UIManager.isSimulating) return;

        if (IsGrounded())
            ProcessMovement();

        ProcessJump();
    }

    private void ProcessMovement()
    {
        playerRb.velocity = new Vector3(inputManager.HorizontalAxis * moveSpeed, playerRb.velocity.y, inputManager.VerticalAxis * moveSpeed);
    }

    private void ProcessJump()
    {
        if (inputManager.IsJumpPressed && IsGrounded())
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
            isJumping = true;
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity -= velGravity * fallMultiplier * Time.deltaTime;
        }
    }

    private bool IsGrounded() => playerGroundCheck.IsGrounded;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //byte[] velocityBytes = VectorSerialization.Vector3ToBytes(playerRb.velocity);
            stream.SendNext(playerRb.velocity);
        }
        else if (stream.IsReading)
        {
            //byte[] velocityBytes = (byte[])stream.ReceiveNext();
            Vector3 decompressedVelocity = (Vector3)stream.ReceiveNext();

            //Vector3 decompressedVelocity = VectorSerialization.BytesToVector3(velocityBytes);
            if (playerRb)
            {
                playerRb.velocity = decompressedVelocity;
            }
        }
    }
}

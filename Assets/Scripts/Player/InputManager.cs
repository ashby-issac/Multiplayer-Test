using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    private bool isJumpPressed;
    private bool isJumpHeld;

    public float HorizontalAxis => horizontal;
    public float VerticalAxis => vertical;

    public bool IsJumpPressed => isJumpPressed;
    public bool IsJumpHeld => isJumpHeld;

    private bool isReadyToClear;

    void Start()
    {
        horizontal = 0;
        vertical = 0;

        isJumpPressed = false;
        isJumpHeld = false;
    }

    void FixedUpdate()
    {
        isReadyToClear = true;
    }

    void Update()
    {
        ResetInputValues();
        ProcessInputs();
    }

    private void ResetInputValues()
    {
        if (!isReadyToClear) return;

        isReadyToClear = false;
        horizontal = 0;
        vertical = 0;

        isJumpPressed = false;
        isJumpHeld = false;
    }

    private void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isJumpHeld = isJumpHeld || Input.GetKey(KeyCode.Space);
        isJumpPressed = isJumpPressed || Input.GetKeyDown(KeyCode.Space);
    }
}
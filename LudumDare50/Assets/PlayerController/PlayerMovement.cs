using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField]
    private Transform movingTransform;

    [SerializeField]
    private float moveTimer = 0.5f;

    [SerializeField]
    private float moveStep = 1.0f;

    private Vector2 move;
    private InputAction movement;

    private float moveTimerAccumulator = 0.0f;
    private bool moved = false;
    private bool canceled = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        playerControls.Enable();
        playerControls.Movement.Enable();
        movement = playerControls.Movement.Move;
        movement.started += OnMoveStarted;
        movement.performed += OnMovePerformed;
        movement.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTimerAccumulator += Time.deltaTime;
        if (moveTimerAccumulator >= moveTimer)
        {
            Move();
            moveTimerAccumulator = 0.0f;
        }
    }

    private void PrepareMove()
    {
        this.move = movement.ReadValue<Vector2>();
        this.moved = false;
        this.canceled = false;
    }

    private void StopMove()
    {
        this.canceled = true;
    }

    private void Move()
    {
        if (this.canceled && this.moved)
        {
            this.move = Vector2.zero;
            return;
        }
        var moveX = this.move.x > 0.0f ? moveStep : this.move.x < 0.0f ? -moveStep : 0.0f;
        var moveZ = this.move.y > 0.0f ? moveStep : this.move.y < 0.0f ? -moveStep : 0.0f;
        var movement = new Vector3(moveX, 0.0f, moveZ);
        movingTransform.position += movement;
        this.moved = true;
    }

    private void OnMoveStarted(InputAction.CallbackContext obj)
    {
        PrepareMove();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        PrepareMove();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        StopMove();
    }
}

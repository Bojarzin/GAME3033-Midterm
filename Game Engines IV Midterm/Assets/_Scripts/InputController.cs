using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float, float>
{

}

public class InputController : MonoBehaviour
{
    PlayerControls controls;
    public MoveInputEvent moveInputEvent;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Movement.Enable();
        controls.Movement.Move.performed += OnMovePerformed;
        controls.Movement.Move.canceled += OnMovePerformed;
    }

    private void OnDisable()
    {
        controls.Movement.Move.performed -= OnMovePerformed;
        controls.Movement.Disable();
    }


    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
        //Debug.Log($"Move Input: {moveInput}");
    }
}

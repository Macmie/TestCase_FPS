using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context) => _playerMovement.Move(context.ReadValue<Vector2>());

    public void OnLook(InputAction.CallbackContext context) => _playerMovement.Look(context.ReadValue<Vector2>());

    public void OnJump(InputAction.CallbackContext context) => _playerMovement.Jump(context);

}

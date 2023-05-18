using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _movementController;
    [SerializeField] private PlayerLookAround _lookAroundController;
    
    private PlayerInput _playerInput;
    private PlayerInput.PlayerControllsActions _playerControlls;
    // Start is called before the first frame update

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerControlls = _playerInput.PlayerControlls;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        _movementController.ProcessMovement(_playerControlls.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _lookAroundController.ProcessLookingAround(_playerControlls.LookingAround.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _playerControlls.Enable();
    }

    private void OnDisable()
    {
        _playerControlls.Disable();
    }
}

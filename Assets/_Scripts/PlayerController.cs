using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerWeaponSelector _playerWeaponSelector;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnMove(InputAction.CallbackContext context) => _playerMovement.Move(context.ReadValue<Vector2>());

    public void OnLook(InputAction.CallbackContext context) => _playerMovement.Look(context.ReadValue<Vector2>());

    public void OnJump(InputAction.CallbackContext context) => _playerMovement.Jump(context);

    public void OnShoot(InputAction.CallbackContext context) => _playerShooting.Shoot(context.phase == InputActionPhase.Started);

    public void OnFirstWeaponChoose(InputAction.CallbackContext context) => _playerWeaponSelector.ChangeWeaponByIndex(0);

    public void OnSecondWeaponChoose(InputAction.CallbackContext context) => _playerWeaponSelector.ChangeWeaponByIndex(1);

    public void OnThirdWeaponChoose(InputAction.CallbackContext context) => _playerWeaponSelector.ChangeWeaponByIndex(2);

    public void ChangeToNextWeapon(InputAction.CallbackContext context) => _playerWeaponSelector.ChangeWeapon(true);

    public void ChangeToPreviousWeapon(InputAction.CallbackContext context) => _playerWeaponSelector.ChangeWeapon(false);

    public void DisableAllInput()
    {
        _playerMovement.DisableMovement();
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
        _playerWeaponSelector.enabled = false;
        this.enabled = false;
    }
}

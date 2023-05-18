using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _playerSpeed;

    private Vector3 _playerVelocity = new Vector3();

    public void ProcessMovement(Vector2 movementInput)
    {
        Vector3 movementVector = Vector3.zero;

        movementVector.x = movementInput.x;
        movementVector.z = movementInput.y;

        _characterController.Move(transform.TransformDirection(movementVector) * _playerSpeed * Time.fixedDeltaTime);
    }
}

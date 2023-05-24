using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _cameraHolder;
    [SerializeField] private float _speed, _sensivity, _maxForce, _maxYLookAngle, _jumpForce;

    public UnityEvent OnJumped;
    public UnityEvent OnLanded;

    private Vector2 _movementVector = new Vector2();
    private Vector2 _lookVector = new Vector2();
    private float _lookRotation;
    private bool _isGrounded = true;

    public void Move(Vector2 movementVector) => _movementVector = movementVector;

    public void Look(Vector2 lookVector) => _lookVector = lookVector;

    public void SetIfGrounded(bool isGrounded)
    {
        HandleJumpLandEvents(isGrounded);
        _isGrounded = isGrounded;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && _isGrounded)
            Jump();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void LateUpdate()
    {
        ProcessLooking();
    }

    private void ProcessMovement()
    {
        if (!_isGrounded) return;
        Vector3 currentVelocity = _rb.velocity;
        Vector3 targetVelocity = new Vector3(_movementVector.x, 0, _movementVector.y);

        targetVelocity = targetVelocity * _speed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        Vector3.ClampMagnitude(velocityChange, _maxForce);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        _rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void ProcessLooking()
    {
        transform.Rotate(Vector3.up * _lookVector.x * _sensivity);

        _lookRotation += (-_lookVector.y * _sensivity);
        _lookRotation = Mathf.Clamp(_lookRotation, -_maxYLookAngle, _maxYLookAngle);
        _cameraHolder.transform.eulerAngles = new Vector3(_lookRotation, _cameraHolder.transform.eulerAngles.y, _cameraHolder.transform.eulerAngles.z);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        SetIfGrounded(false);
    }
    
    private void HandleJumpLandEvents(bool isGrounded)
    {
        bool landed = !_isGrounded && isGrounded;
        bool jumped = _isGrounded && !isGrounded;

        if (landed)
            OnLanded?.Invoke();
        if (jumped)
            OnJumped?.Invoke();
    }
}

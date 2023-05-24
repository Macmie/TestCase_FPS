using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SoundController : MonoBehaviour
{
    public UnityEvent OnStartWalking;
    public UnityEvent OnStopWalking;
    public UnityEvent OnPistolShot;
    public UnityEvent OnRifleShot;
    public UnityEvent OnShotgunShot;

    public void OnWalkingStateChange(InputAction.CallbackContext context) => HandleWalkingSounds(context);

    public void PistolShot() => OnPistolShot?.Invoke();

    public void RifleShot() => OnRifleShot?.Invoke();

    public void ShotgunShot() => OnShotgunShot?.Invoke();

    private void HandleWalkingSounds(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnStartWalking?.Invoke();

        else if (context.phase == InputActionPhase.Canceled)
            OnStopWalking?.Invoke();
    }
}

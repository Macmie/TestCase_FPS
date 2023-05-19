using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WalkableEnvironment"))
            _playerMovement.SetIfGrounded(true);
    }
}

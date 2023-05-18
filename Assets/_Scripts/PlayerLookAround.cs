using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{
    [SerializeField] private Camera _FPVCamera;
    [SerializeField] private float xRotationSpeed;
    [SerializeField] private float yRotationSpeed;
    [SerializeField] private float xRotationMin;
    [SerializeField] private float xRotationMax;

    private float xRotation = 0f;
    // Start is called before the first frame update
    public void ProcessLookingAround (Vector2 input)
    {
        float lookX = input.x;
        float lookY = input.y;

        xRotation -= (lookY * Time.deltaTime) * yRotationSpeed;
        xRotation = Mathf.Clamp(xRotation, xRotationMin, xRotationMax);

        _FPVCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookX * Time.deltaTime) * xRotationSpeed);
    }
}

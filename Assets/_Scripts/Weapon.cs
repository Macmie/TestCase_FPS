using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemMaterial _materialsToImpact;
    [SerializeField] private int _damage;
    [SerializeField] private Vector3 _shootSpread;
    [SerializeField] private ParticleSystem _burstEffect;
    [SerializeField] private ParticleSystem _impactEffect;
    [SerializeField] Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] Animator _animator;

    public UnityAction HideAction;
    public bool IsInTransition;
    private int _mask;



    private void OnEnable()
    {
        IsInTransition = true;
    }

    private void OnHidden()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _mask = 1 << _layerMask.value;
    }

    public void HideGun()
    {
        _animator.SetTrigger("Hide");
        Debug.Log("Start Gun Hiding");
    }

    public void Shoot(bool shoot)
    {
        if (!shoot || IsInTransition) return;
        Debug.Log($"Shoot {gameObject.name}");
        _burstEffect.Play();
        Vector3 spread = GetSpread();
        Vector3 shootDir = (_burstEffect.transform.forward + spread).normalized;
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _mask))
        {
            Debug.Log($"Hit {hitInfo.collider.name}");

            hitInfo.collider.TryGetComponent(out Destructable destructable);
            if (destructable != null)
            {
                MakeDamage(destructable, _materialsToImpact);
                Color color = destructable.GetColor();
                PlayImpactEffect(shootDir, hitInfo, color);
                return;
            }
            PlayImpactEffect(shootDir, hitInfo, default);
        }
    }


    private void PlayImpactEffect(Vector3 shootDir, RaycastHit hitInfo, Color color)
    {
        Vector3 incomingVec = hitInfo.point - _mainCamera.transform.position;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hitInfo.normal);

        ParticleSystem impact = Instantiate(_impactEffect, hitInfo.point, Quaternion.FromToRotation(Vector3.up, reflectVec));
        ParticleSystemRenderer render = impact.GetComponent<ParticleSystemRenderer>();
        render.material.color = color != default ? color : render.material.color;
        impact.Play();
    }

    private Vector3 GetSpread()
    {
        float x = Random.Range(-_shootSpread.x, _shootSpread.x);
        float y = Random.Range(-_shootSpread.y, _shootSpread.y);
        float z = Random.Range(-_shootSpread.z, _shootSpread.z);

        return new Vector3(x, y, z);
    }

    public void EnableWeapon(bool enable) => gameObject.SetActive(enable);

    private void MakeDamage(Destructable destructable, ItemMaterial material)
    {
        destructable.TakeDamage(_damage, material);
    }
}

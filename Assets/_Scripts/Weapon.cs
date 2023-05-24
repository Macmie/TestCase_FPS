using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IImpactMaterial
{

    [SerializeField] [Tooltip("In Seconds")] private float _shootSpeed;
    [SerializeField] private ItemMaterial _materialsToImpact;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _burstEffect;
    [SerializeField] private ParticleSystem _impactEffect;
    [SerializeField] Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] Animator _animator;
    [SerializeField] Renderer _objectRenderer;

    public UnityEvent HideAction;
    public UnityEvent OnShoot;

    public bool IsInTransition;
    private int _mask;
    private float _timeToNextShot;
    private bool _canShoot, _startShooting;

    private void Awake() => _mask = 1 << _layerMask.value;

    private void Update()
    {
        HandleIfCanShoot();
        Shoot(_startShooting);
    }

    private void OnEnable()
    {
        IsInTransition = true;
        _startShooting = false;
        _timeToNextShot = 0f;
    }

    private void HandleIfCanShoot()
    {
        if (!_canShoot && _timeToNextShot < .01f)
            _canShoot = true;

        if (_timeToNextShot > .1f)
            _timeToNextShot -= Time.deltaTime;
        else
            _timeToNextShot = 0f;
    }

    private void OnHidden() => gameObject.SetActive(false);

    public void HideGun() => _animator.SetTrigger("Hide");

    public void StartShooting(bool startShooting) => _startShooting = startShooting;

    public void Shoot(bool shoot)
    {
        if (!shoot || IsInTransition || !_canShoot ) return;

        OnShoot?.Invoke();
        _burstEffect.Play();
        _canShoot = false;
        _timeToNextShot = _shootSpeed;

        Ray ray = _mainCamera.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _mask))
        {
            hitInfo.collider.TryGetComponent(out Destructable destructable);
            if (destructable != null)
            {
                MakeDamage(destructable, _materialsToImpact);
                Color color = destructable.GetColor();
                PlayImpactEffect(hitInfo, color);
                return;
            }
            PlayImpactEffect(hitInfo, default);
        }
    }

    private void PlayImpactEffect(RaycastHit hitInfo, Color color)
    {
        Vector3 incomingVec = hitInfo.point - _mainCamera.transform.position;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hitInfo.normal);

        ParticleSystem impact = Instantiate(_impactEffect, hitInfo.point, Quaternion.FromToRotation(Vector3.up, reflectVec));
        ParticleSystemRenderer render = impact.GetComponent<ParticleSystemRenderer>();
        render.material.color = color != default ? color : render.material.color;
        impact.Play();
    }

    public void EnableWeapon(bool enable) => gameObject.SetActive(enable);

    private void MakeDamage(Destructable destructable, ItemMaterial material) => destructable.TakeDamage(_damage, material);

    public ItemMaterial GetMaterial() => _materialsToImpact;

    public void SetRenderColorFromMaterial(Color color) => _objectRenderer.material.color = color;
}

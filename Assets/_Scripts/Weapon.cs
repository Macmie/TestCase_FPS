using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Material _materialsToImpact;
    [SerializeField] private int _damage;
    [SerializeField] private Vector3 _shootSpread;
    [SerializeField] private ParticleSystem _burstEffect;
    [SerializeField] private LayerMask _layerMask;

    private int _mask;

    private void Awake()
    {
        _mask = 1 << _layerMask.value;
    }

    public void Shoot(bool shoot)
    {
        if (!shoot) return;
        Debug.Log($"Shoot {gameObject.name}");
        _burstEffect.Play();
        Vector3 spread = GetSpread();
        Ray ray = new Ray(_burstEffect.transform.position, _burstEffect.transform.forward + spread);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _mask))
        {
            Debug.Log($"Hit {hitInfo.collider.name}");
            hitInfo.collider.TryGetComponent(out Destructable destructable);
            if (destructable == null) return;

            MakeDamage(destructable);
        }
    }

    private Vector3 GetSpread()
    {
        float x = Random.Range(-_shootSpread.x, _shootSpread.x);
        float y = Random.Range(-_shootSpread.y, _shootSpread.y);
        float z = Random.Range(-_shootSpread.z, _shootSpread.z);

        return new Vector3(x, y, z);
    }

    private void MakeDamage(Destructable destructable)
    {
        destructable.TakeDamage(_damage);

        //TODO HitImpact Effect
    }
}

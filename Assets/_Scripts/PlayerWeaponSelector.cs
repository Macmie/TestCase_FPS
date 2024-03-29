using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelector : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private PlayerShooting _playerShooting;

    private bool _isChanging = false;

    private int _activeWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetWeapon(0));
    }

    public void ChangeWeaponByIndex(int index)
    {
        if (index == _activeWeaponIndex || _isChanging) return;

        if (index < 0 || index >= _weaponList.Count)
            return;

        StartCoroutine(SetWeapon(index));
    }

    public void ChangeWeapon(bool next)
    {
        if (_isChanging) return;
        int index = next ? _activeWeaponIndex + 1 : _activeWeaponIndex - 1;
        if (next)
            index = index >= _weaponList.Count ? 0 : index;
        else
            index = index <= 0 ? _weaponList.Count - 1 : index;

        StartCoroutine(SetWeapon(index));
    }

    IEnumerator SetWeapon(int index)
    {
        _isChanging = true;
        Weapon prevGun = _weaponList[_activeWeaponIndex];
        if (prevGun.gameObject.activeInHierarchy)
        {
            prevGun.HideGun();
            while (prevGun.gameObject.activeInHierarchy)
                yield return null;
        }

        Weapon newGun = _weaponList[index];
        newGun.gameObject.SetActive(true);
        while (newGun.IsInTransition)
            yield return null;

        _playerShooting.SetActiveWeapon(newGun);
        _activeWeaponIndex = index;
        _isChanging = false;
    }
}

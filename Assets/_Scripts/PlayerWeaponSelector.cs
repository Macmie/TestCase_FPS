using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelector : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private PlayerShooting _playerShooting;

    private int _activeWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(0);
    }

    public void ChangeWeaponByIndex(int index)
    {
        if (index == _activeWeaponIndex) return;
        if (index < 0 || index >= _weaponList.Count)
        {
            Debug.LogError("Weapon index outside the weapon array bounds!");
            return;
        }
        SetWeapon(index);
    }

    public void ChangeWeapon(bool next)
    {
        int index = next ? _activeWeaponIndex + 1 : _activeWeaponIndex - 1;
        if (next)
            index = index >= _weaponList.Count ? 0 : index;
        else
            index = index <= 0 ? _weaponList.Count - 1 : index;

        SetWeapon(index);
    }

    private void SetWeapon(int index)
    {
        //Todo changing weapon effect
        _weaponList[_activeWeaponIndex].gameObject.SetActive(false);
        Debug.Log($"Set active weapon to {_weaponList[index].gameObject.name}");
        _playerShooting.SetActiveWeapon(_weaponList[index]);
        _activeWeaponIndex = index;
        _weaponList[_activeWeaponIndex].gameObject.SetActive(true);
    }
}

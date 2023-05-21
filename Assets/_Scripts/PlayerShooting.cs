using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Weapon _activeWeapon;


    public void SetActiveWeapon(Weapon weapon) => _activeWeapon = weapon;

    public void Shoot(bool shoot) => _activeWeapon.Shoot(shoot);
}

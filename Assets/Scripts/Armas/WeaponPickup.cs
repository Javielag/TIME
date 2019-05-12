using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public Weapon thisWeapon;
    public Animator anim;

    public Weapon WhatWeapon()
    {
        return thisWeapon;
    }
    public void OnPicked()
    {
        Destroy(this.gameObject);
    }
}

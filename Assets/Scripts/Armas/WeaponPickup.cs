using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public Weapon thisWeapon;
    public Weapon WhatWeapon()
    {
        return thisWeapon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        //transform.Find("PickIcon").gameObject.SetActive(false);
    }
    public void OnPicked()
    {
        Destroy(this.gameObject);
    }
}

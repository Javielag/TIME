using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Municion : MonoBehaviour {

    public float ammoPercentage;
    WeaponManager wm;
    public void Interacted()
    {
        wm = GameManager.instance.GetPlayer().GetComponentInChildren<WeaponManager>();
        if (wm)
        {
            wm.UpgradeMagSize(ammoPercentage);
            GameManager.instance.UpdatePerk("Recarga");
        }
    }
}

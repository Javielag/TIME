using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Cadencia : MonoBehaviour {

    public float cadence;
    WeaponManager wm;
    public void Interacted()
    {
        wm = GameManager.instance.GetPlayer().GetComponentInChildren<WeaponManager>();
        if (wm)
        {
            wm.UpgradeFireRate(cadence);
            GameManager.instance.UpdatePerk("Cadencia");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    Gun weapon;
    float changeTime;
    public bool isSwitching = false;
    [SerializeField]int currentWeapon=0;
    IEnumerator reload;
    Weapon[] equipadas = new Weapon[] {Weapon.pistola,Weapon.revolver};
    private void Start()
    {
        changeTime = GetComponentInParent<PlayerController>().changeTime;
        CancelReload();
    }
    public IEnumerator SwitchWeapon(float changeTime)
    {
        Debug.Log("Cambiando arma");
        currentWeapon = (currentWeapon + 1) % equipadas.Length;
        weapon = GetComponentInChildren<Gun>();
        weapon.CannotShoot();
        GameManager.instance.SwitchingWeaponUI();
        if (IsReloading())
        {
            Debug.Log("Recarga cancelada");
            CancelReload();
        }
        isSwitching = true;
        yield return new WaitForSeconds(changeTime);
        SelectWeapon(currentWeapon);
    }
    public void ChangeWeapon(Collider2D wpCol)
    {
        if (!isSwitching)                                               //Evita recoger un arma cuando esté cambiando por la secundaria
        {
            WeaponPickup wpPick = wpCol.GetComponent<WeaponPickup>();
            if (wpPick)
            {
                equipadas[currentWeapon] = wpPick.WhatWeapon();
                SelectWeapon(currentWeapon);
                wpPick.OnPicked();
            }
        }
        
    }
    private void SelectWeapon(int weapn)                //Activa el arma weapn y desactiva todas las demás
    {
        foreach (Transform weap in transform)
        {
            if (weap.GetComponentInChildren<Gun>())         //No lo hace si el hijo no tiene el componente Gun, para evitar al Melee
            {
                if (weap.GetComponentInChildren<Gun>())
                {
                    if (equipadas[weapn] == weap.GetComponentInChildren<Gun>().iAmWeapon)
                    {
                        weap.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (weap.gameObject.name == equipadas[(currentWeapon + 1) % equipadas.Length].ToString())
                        {
                            weap.gameObject.GetComponentInChildren<Gun>().Disable();
                        }
                        weap.gameObject.SetActive(false);
                    }
                }                
            }            
        }
        isSwitching = false;
        GameManager.instance.ChangeWeapon(equipadas[weapn], equipadas[(weapn+1)%equipadas.Length]);
    }
    public void CancelReload()                      
    {
        if (IsReloading())
        {
            weapon = GetComponentInChildren<Gun>();
            StopCoroutine(reload);
            weapon.NotReload();
            GameManager.instance.ReloadingIconUI(false);
        }

    }
    public void StartReload()
    {
        if (!isSwitching && !CheckAmmo())
        {
            weapon = GetComponentInChildren<Gun>();
            reload = weapon.Reload();
            StartCoroutine(reload);
            GameManager.instance.ReloadingIconUI(true);
        }
    }
    void Swap(int[] a)
    {
        int x = a[0];
        a[0] = a[1];
        a[1] = x;
    }
    public bool CheckAmmo()            //Le pregunta al arma equipada si tiene munición máxima en el cargador
    {
        weapon = GetComponentInChildren<Gun>();
        return weapon.MaxAmmo();
    }
    public bool IsReloading()          //Le pregunta al arma si está recargando
    {
        weapon = GetComponentInChildren<Gun>();
        return weapon.Reloading();
    }
    public void IsSwitching()
    {
        isSwitching = true;
    }
    public Weapon SecondaryWeapon()
    {
        return equipadas[0];
    }
    public void StartWeaponSwitch()
    {
        StartCoroutine("SwitchWeapon", changeTime);
    }
    public void UpgradeFireRate(float percentage)
    {
        foreach(Transform child in transform)
        {
            Gun gun = child.GetComponentInChildren<Gun>();
            if (gun)
            {
                gun.rate -= gun.rate * (percentage / 100);
            }
        }
    }
    public void UpgradeMagSize(float percentage)
    {
        foreach (Transform child in transform)
        {
            Gun gun = child.GetComponentInChildren<Gun>();
            if (gun)
            {
                gun.magSize += Mathf.RoundToInt(gun.magSize*(percentage/100));
                if (gun.gameObject.activeSelf)
                {
                    GameManager.instance.UpdateMaxAmmo(gun.magSize);
                }
            }
        }
    }

}

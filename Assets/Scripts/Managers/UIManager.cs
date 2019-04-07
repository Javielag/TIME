﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text healthText;
    public Transform healthBar, healthBarTop, switchIcon, reloadIcon, healthContainer, perkCadencia, perkVelocidad, perkRecarga, perkVida;    
    public Text oleada;
    public Text ammo1, mag1, ammo2, mag2;
    public Transform ArmaPrincipal, ArmaSecundaria;
    int maxHealth;
    void Start ()
    {
        GameManager.instance.SetUI(this.gameObject);
        maxHealth = GameManager.instance.player.GetComponent<Health>().GetMaxHealth();
        UpdateHealth(maxHealth);
	}
	
	// Update is called once per frame
	public void UpdateHealth(float newHealth)
    {
        healthText.text = newHealth.ToString();
        healthBar.localScale = new Vector3(newHealth/maxHealth, 1);
        healthBarTop.localScale = new Vector3(-maxHealth/newHealth, -1);
        Debug.Log(newHealth / maxHealth);
	}
    public void ChangeWeaponUI(Weapon weaponPrincipal, Weapon weaponSecondary)
    {
        //Si la secundaria es nothing, desactiva la caja entera, si no, la activa IMPORTANTE me parece ultrafeo esta parte del código así que cuando toque limpieza hay que mirarlo MUY FUERTE
        if (weaponSecondary == Weapon.nothing)
        {
            ArmaSecundaria.gameObject.SetActive(false);
        }
        else ArmaSecundaria.gameObject.SetActive(true);
        foreach (Transform weap in ArmaPrincipal)
        {
            if (weaponPrincipal.ToString() == weap.gameObject.name || weap.gameObject.name == "Ammo")
            {
                weap.gameObject.SetActive(true);
            }
            else
            {
                weap.gameObject.SetActive(false);
            }
        }
        foreach (Transform weap in ArmaSecundaria)
        {
            if (weaponSecondary.ToString() == weap.gameObject.name || weap.gameObject.name == "Ammo")
            {
                weap.gameObject.SetActive(true);
            }
            else
            {
                weap.gameObject.SetActive(false);
            }          
        }
    }
    public void UpdateCurrentAmmo(int ammo)
    {
        ammo1.text = ammo.ToString();
    }
    public void UpdateSecondaryAmmo(int ammo, int maxAmmo)
    {
        ammo2.text = ammo.ToString();
        mag2.text = maxAmmo.ToString();
    }
    private void SwapAmmo()             //QUITAR EN UN FUTURO
    {
        string auxAmmo, auxMag;

        auxAmmo = ammo1.text;
        auxMag = mag1.text;

        ammo1.text = ammo2.text;
        mag1.text = mag2.text;

        ammo2.text = auxAmmo;
        mag2.text = auxMag;
        switchIcon.gameObject.SetActive(true);
    }
    public void UpdateMaxAmmo(int ammo)
    {
        mag1.text = ammo.ToString();
    }
    public void UpdateSecondaryWeapon(int ammo, int maxAmmo)
    {
        ammo2.text = ammo.ToString();
        mag2.text = maxAmmo.ToString();
    }
    public void UpdateOleada(int newOleada)
    {
        oleada.text = "Oleada: " + newOleada.ToString();
    }
    public void SwitchingWeapon()
    {
        switchIcon.gameObject.SetActive(true);
    }
    public void ReloadingWeapon(bool state)
    {
        reloadIcon.gameObject.SetActive(state);
    }
    public void UpdateMaxHealthVariable(int newMax)
    {
        maxHealth = newMax;
        UpdateScale();
    }
    //Dependiendo del perk que haya recogido activa el correspondiente
    public void ActivatePerk(string perk)
    {
        switch (perk)
        {
            case "Vida": perkVida.gameObject.SetActive(true); break;
            case "Cadencia": perkCadencia.gameObject.SetActive(true); break;
            case "Velocidad": perkVelocidad.gameObject.SetActive(true); break;
            case "Recarga": perkRecarga.gameObject.SetActive(true); break;
        }
    }
    //Cambia la escala de HealthContainer y mantiene su texto a su escala original
    void UpdateScale()
    {
        healthContainer.localScale = new Vector3(1.25f, 1);
        healthText.transform.localScale = new Vector3(0.8f, 1);
    }
   
}

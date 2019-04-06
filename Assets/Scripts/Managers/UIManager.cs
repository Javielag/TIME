using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text healthText;
    public Transform healthBar, switchIcon, reloadIcon;    
    public Text oleada;
    public Text ammo1, mag1, ammo2, mag2;
    public Text avisoportal;
    public Transform avportal;
    public Transform ArmaPrincipal, ArmaSecundaria;
    [SerializeField]int maxHealth, timerPortales;
    bool avisoPortal = false;
    IEnumerator portalCoroutine;
    void Start ()
    {
        GameManager.instance.SetUI(this.gameObject);
        maxHealth = GameManager.instance.player.GetComponent<Health>().maxHealth;
        UpdateHealth(maxHealth);
	}
	
	// Update is called once per frame
	public void UpdateHealth(float newHealth)
    {
        healthText.text = newHealth.ToString();
        healthBar.localScale = new Vector3(newHealth/maxHealth, 1);
	}
    public void ChangeWeaponUI(Weapon weaponPrincipal, Weapon weaponSecondary)
    {
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
    private void SwapAmmo()
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
    public void AvisoPortal(string pos)
    {
        portalCoroutine = (AvisoPortalCorrutina(pos));
        StartCoroutine(portalCoroutine);
    }
    IEnumerator AvisoPortalCorrutina(string pos)
    {
        yield return new WaitWhile(AvisoIsActive);              //Hasta que el método AvisoIsActive sea false
        avisoPortal = true;
        avportal.gameObject.SetActive(true);
        avisoportal.text = "Ha aparecido un portal en la sala de" + pos;
        Invoke("DesactivaTexto", timerPortales);
    }
    public void DesactivaTexto()
    {
        avportal.gameObject.SetActive(false);
        avisoPortal = false;
    }
    bool AvisoIsActive()
    {
        if (avisoPortal) return true;
        else return false;
    }
    
   
}

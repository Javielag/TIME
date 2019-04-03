using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CienciaManager : MonoBehaviour {

    public int primeraAparicion, cadaX;
    public float perkChance=0.8f;//probabilidad de que te toque una mejora, en caso contrario un arma
    public WeaponPickup[] weapons = new WeaponPickup[6];//todos los pickups de armas
    public GameObject[] perks = new GameObject[4];//todas las mejoras
    private bool[] perkGiven = new bool[4];//indica true cuando la mejora en el mismo sitio ya ha sido otorgada
    WeaponManager wm;
    public Cientifico prefabCientifico;
    public Transform posCientifico;
    int count =0;

    private void Start()
    {
        GameManager.instance.SetCienciaManager(this);
    }
    public void Visita()
    {
        count++;
        Debug.Log(count);
        Cientifico cientifico = Instantiate<Cientifico>(prefabCientifico,posCientifico.position,Quaternion.identity,transform);
        if ((Random.Range(0f, 1f) <= perkChance) && MissingPerks())
        {
            int spawnPerk;
            //elige un perk sin repetir
            do
            {
                spawnPerk = Random.Range(0, perks.Length);
            } while (perkGiven[spawnPerk]);
            cientifico.Ofrece(perks[spawnPerk]);
            perkGiven[spawnPerk] = true;
        }
        else
        {
            wm = GameManager.instance.GetPlayer().GetComponentInChildren<WeaponManager>();
            int n;
            do
            {
                n = Random.Range(0, weapons.Length);
            } while (weapons[n].thisWeapon == wm.equipedWeapon(0) || weapons[n].thisWeapon == wm.equipedWeapon(1));
            cientifico.Ofrece(weapons[n].gameObject);
        }
    }
    private bool MissingPerks()
    {
        int i = 0;
        while (i < perkGiven.Length && perkGiven[i])
            i++;
        return (i < perkGiven.Length);  
    }
}

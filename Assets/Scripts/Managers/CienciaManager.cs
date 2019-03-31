using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CienciaManager : MonoBehaviour {

    public int primeraAparicion, cadaX;
    public float perkChance=20f;//probabilidad de que te toque una mejora, en caso contrario un arma
    public WeaponPickup[] weapons = new WeaponPickup[6];//todos los pickups de armas
    public GameObject[] perks = new GameObject[4];//todas las mejoras
    public bool[] perkGiven = new bool[4];//indica true cuando la mejora en el mismo sitio ya ha sido otorgada
    WeaponManager wm;

    public void Visita()
    {
        if (Random.Range(0f, 1f) <= perkChance && MissingPerks())
        {
            int spawnPerk;
            //elige un perk sin repetir
            do
            {
                spawnPerk = Random.Range(0, perks.Length-1);
            } while (perkGiven[spawnPerk]);
            Instantiate(perks[spawnPerk], transform.position, Quaternion.identity, transform);
        }
        else
        {
            wm = GameManager.instance.GetPlayer().GetComponentInChildren<WeaponManager>();
            int n;
         //   do
          //  {
                n = Random.Range(0, weapons.Length - 1);
          //  }while(weapons[n].thisWeapon ==wm.)
        }
    }
    private bool MissingPerks()
    {
        int i = 0;
        while (i < perkGiven.Length && perkGiven[i])
            i++;
        return !(i < perkGiven.Length);  
    }
}

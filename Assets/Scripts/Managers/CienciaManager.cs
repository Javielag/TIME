using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CienciaManager : MonoBehaviour
{

    public int primeraAparicion, cadaX;
    public float perkChance = 0.8f;//probabilidad de que te toque una mejora, en caso contrario un arma
    public WeaponPickup[] weapons = new WeaponPickup[6];//todos los pickups de armas
    public GameObject[] perks = new GameObject[4];//todas las mejoras
    private int[] perkOrder; int perkCount = 0;
    private int wpCount=1; //solo se usa en modo debug, se salta la pistola
    public bool DEBUG;
    WeaponManager wm;
    public Cientifico prefabCientifico;
    public Transform posCientifico;
    int count = 0;

    private void Start()
    {
        ImCienciaManager();
        if(!DEBUG)
            perkOrder = shufflePerks(perks);
    }
    public void Visita()
    {
        Cientifico cientifico = Instantiate<Cientifico>(prefabCientifico, posCientifico.position, Quaternion.identity, transform);
        if (!DEBUG)
        {
            Debug.Log(StackTraceUtility.ExtractStackTrace());
            count++;
            Debug.Log(count);

            //si toca perk
            if ((Random.Range(0f, 1f) <= perkChance) && MissingPerks())
            {
                cientifico.Ofrece(perks[perkOrder[perkCount]]);
                perkCount++;
            }
            //si toca arma
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
        else
        {
            if (perkCount < perks.Length) {
                cientifico.Ofrece(perks[perkCount]);
                perkCount++;
            }
            if (wpCount < weapons.Length)
            {
                cientifico.Ofrece2(weapons[wpCount]);
                wpCount++;
            }

        }
    }
    private bool MissingPerks()
    {
        return (perkCount < perkOrder.Length);
    }
    private int[] shufflePerks(GameObject[] perks)
    {
        int[] shuffled = new int[perks.Length];
        //inicializa los valores del array
        for (int i = 0; i < shuffled.Length; i++) { shuffled[i] = i; }
        for (int i = 0; i < shuffled.Length; i++)
        {
            int aux = shuffled[i];
            int rnd = Random.Range(i, shuffled.Length);
            shuffled[i] = shuffled[rnd];
            shuffled[rnd] = aux;
        }
        return shuffled;
    }
    private void ImCienciaManager()
    {
        GameManager.instance.SetCienciaManager(this);
    }
}
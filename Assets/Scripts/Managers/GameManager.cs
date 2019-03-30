using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public GeneradorOleadas generadorOleadas;
    public GameObject player;
    public int enemyCount;
    [SerializeField]private int /*enemyCount,*/oleadaActual=-1;
    public float delayOleada;
    [SerializeField]
    UIManager UI;

    //Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

    }
    // Use this for initialization
    public void Start()
    {
        Invoke("GeneraOleada",delayOleada);
    }


    public void ChangeHealth(int value, GameObject target)
    {
        Health tgHealth = target.GetComponent<Health>();
        if (tgHealth){
            tgHealth.ChangeHealth(value);
            //si es el jugadir quien ha recibido daño
            if (tgHealth.GetComponent<PlayerController>())
            {
                UI.UpdateHealth(tgHealth.health);
            }
        }
        
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
    public void SetUI(GameObject newUI)
    {
        UI = newUI.GetComponent<UIManager>();
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    public void ChangeWeapon(Weapon weaponPrincipal, Weapon weaponSecondary)
    {
        UI.ChangeWeaponUI(weaponPrincipal, weaponSecondary);
    }
    //cada vez que muere un enemigo
    public void EnemySlain()
    {
        enemyCount--;
        //si era el último, genera una nueva oleada
        if (enemyCount <= 0)
        {
       
            if ((oleadaActual + 5) % 10 == 0)
            {
                //aquí van las visitas del CIENTEFRICO
                Invoke("GeneraOleada", delayOleada);
            }
            else
            {
                Invoke("GeneraOleada", delayOleada);
            }
        }
          
    }
    //Ordena generar una oleada
    public void GeneraOleada()
    {
        oleadaActual++;
        UI.UpdateOleada(oleadaActual);
        generadorOleadas.GeneraOleada(oleadaActual,out enemyCount);
    }
    //añade enemigos a la oleada(ej. portales)
    public void XtraEnemies(int n)
    {
        enemyCount += n;
    }
    public void UpdateAmmo(int ammo)
    {
        UI.UpdateCurrentAmmo(ammo);
    }
    public void UpdateMaxAmmo(int ammo)
    {
        UI.UpdateMaxAmmo(ammo);
    }
    public void DisableWeapon(int ammo, int maxAmmo)
    {
        UI.UpdateSecondaryWeapon(ammo, maxAmmo);
    }
    public void SwitchingWeaponUI()
    {
        UI.SwitchingWeapon();
    }
    public void ReloadingIconUI(bool state)
    {
        UI.ReloadingWeapon(state);
    }
    public void UpdateSecondaryAmmo(int ammo, int maxAmmo)
    {
        UI.UpdateSecondaryAmmo(ammo, maxAmmo);
    }
}

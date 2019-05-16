using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    //private bool portals;
    private GeneradorOleadas generadorOleadas;
    private CienciaManager cientifico;
    public GameObject player;
    public GameObject a, b, c, d;
    private int enemyCount;
    [SerializeField]private int /*enemyCount,*/oleadaActual=0;
    public float delayOleada;
    bool menuPausa;
    [SerializeField]
    UIManager UI;
    public bool canTeleport = true;
    public int record = -1;

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
        LoadScore();
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
        if (enemyCount >= 0)
        {
            enemyCount--;
            //si era el último, genera una nueva oleada
            if (enemyCount <= 0)
            {
                if ((oleadaActual - cientifico.primeraAparicion) % cientifico.cadaX == 0)
                {
                    //aquí van las visitas del CIENTEFRICO
                    cientifico.Visita();
                }
                else
                {
                    GeneraOleada();
                }
            }
        }
          
    }
    //Ordena generar una oleada
    public void GeneraOleada()
    {
        if (oleadaActual == 0) UI.UpdateRecord(record);
        oleadaActual++;
        UI.UpdateOleada(oleadaActual);
        if (oleadaActual - 1 > record) UI.UpdateRecord(oleadaActual - 1);
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
    public void AvisoPortal(string pos)
    {
        UI.AvisoPortal(pos);
    }
    public GameObject GetA()
    {
        return a;
    }
    public GameObject GetB()
    {
        return b;
    }
    public GameObject GetC()
    {
        return c;
    }
    public GameObject GetD()
    {
        return d;
    }
    public void UpdateSecondaryAmmo(int ammo, int maxAmmo)
    {
        UI.UpdateSecondaryAmmo(ammo, maxAmmo);
    }
    public void UpdateMaxHealth(int newMax)
    {
        UI.UpdateMaxHealthVariable(newMax);
    }
    public void UpdatePerk(string perk)
    {
        UI.ActivatePerk(perk);
    }
    public void SetCienciaManager(CienciaManager dis)
    {
        cientifico = dis;
    }
    public void SetGeneraOleadas(GeneradorOleadas olas)
    {
        generadorOleadas = olas;
    }
    public void ChangeScene(string button)
    {
       
        Time.timeScale = 1;                 //Evita errores con el menú
        menuPausa = false;
        if (button == "Menu")               //Si vuelve al menú principal, reinicia las rondasy guarda el record
        {
            CancelInvoke();
            SaveScore();
            oleadaActual = 0;
            if(generadorOleadas != null)
            {
                generadorOleadas.AcabaOleada();
            }
            Cursor.visible = true;
        }
        if (button == "Hub") Cursor.visible = false;
        SceneManager.LoadScene(button);     //Carga la escena

    }
    void pauseGame()            //Para el juego
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;               //Si el tiempo avanza, lo para, y viceversa

    }
    public void ChangeMenuState()
    {
        //Invierte el booleano de menú y activa/desactiva el menú
        menuPausa = !menuPausa;
        Cursor.visible = !Cursor.visible;
        UI.PauseMenu(menuPausa);
        pauseGame();
    }
    public bool GetPauseState()
    {
        return menuPausa;
    }
    public void ExitGame()
    {
        SaveScore();
        Application.Quit();
    }
    public void SetOleada(int estaOleada)
    {
        oleadaActual = estaOleada;
    }
    public void SaltaOleada()
    {
        Transform enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").transform;
        foreach (Transform enemy in enemyPool)
        {
            Destroy(enemy.gameObject);
        }
        generadorOleadas.AcabaOleada();
        //int actOl = oleadaActual;
        for (int i = enemyCount; i > 0; i--)
        {
            EnemySlain();
        }
    }
    public void Description(string description)
    {
        UI.ShowDescription(description);
    }
    public void ExtraEnemies(int xtra) { enemyCount += xtra; }
    void LoadScore()
    {
        if (PlayerPrefs.HasKey("record"))
        {
            record = PlayerPrefs.GetInt("record");
        }
    }
    public void SaveScore()
    {
        if(oleadaActual - 1 > record)
        {
            PlayerPrefs.SetInt("record", oleadaActual - 1);
            record = oleadaActual - 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour {
    public PortalMago PortalMagoPrefab;
    public float tiempoCasteo, tiempoCooldown;
    GameObject player;
    bool generated = false;
    IEnumerator coroutine;
    RangeEnemy thisEnemy;
    Transform bulletPool;
    void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        thisEnemy = GetComponent<RangeEnemy>();
        player = GameManager.instance.GetPlayer();            
    }
    void Update()
    {   //Si no hay uno creado y no se está moviendo
        if (!generated && !thisEnemy.Moving())
        {
            Debug.Log("Iniciando casteamiento");
            thisEnemy.SetCanMove(false);        //Impide el movimiento
            generated = true;
            Invoke("CreaPortales", 0);
        }
    }
    void CreaPortales()
    {
        PortalMago portalOfensivo = Instantiate(PortalMagoPrefab, player.transform.position, Quaternion.identity, bulletPool);
        PortalMago portalDefensivo = Instantiate(PortalMagoPrefab, transform.position, Quaternion.identity, bulletPool);                //Crea ambos portales
        coroutine = ActivaPortales(portalOfensivo, portalDefensivo);
        StartCoroutine(coroutine);        
    }
    IEnumerator ActivaPortales(PortalMago portal1, PortalMago portal2)
    {
        if (GameManager.instance.canTeleport) //Solo uno de los magos puede realizar el teletransporte pero visualmente se ven todos, poner más arriba si se quiere que no se vean
        {
            GameManager.instance.canTeleport = false;
            yield return new WaitForSeconds(tiempoCasteo);                          //Espera el tiempo indicado
            portal1.Teleport(portal2.transform.position); portal2.Teleport(portal1.transform.position);                 //Realiza el teletransporte
            thisEnemy.SetCanMove(true);                                             //Les deja moverse
            Invoke("PuedeGenerar", tiempoCooldown);                                   //Tras un tiempo puede volver a crear portales
        }
        else Invoke("PuedeGenerar", tiempoCasteo + tiempoCooldown);
    }
    void PuedeGenerar()
    {
        generated = false;
        GameManager.instance.canTeleport = true;
    }
}

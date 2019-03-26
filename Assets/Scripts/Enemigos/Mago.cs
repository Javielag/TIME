using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour {
    public PortalMago PortalMagoPrefab;
    public float tiempoCasteo;
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
        portalOfensivo.SetOtherPortal(portalDefensivo.transform.position);                                                              //Les informa de la existencia del otro
        portalDefensivo.SetOtherPortal(portalOfensivo.transform.position);
        coroutine = ActivaPortales(portalOfensivo, portalDefensivo);
        StartCoroutine(coroutine);
        
    }
    IEnumerator ActivaPortales(PortalMago portal1, PortalMago portal2)
    {
        yield return new WaitForSeconds(tiempoCasteo);                          //Espera el tiempo indicado
        portal1.SetActive(true); portal2.SetActive(true);                       //Los activa
        thisEnemy.SetCanMove(true);                                             //Les deja moverse
        Invoke("PuedeGenerar", tiempoCasteo);                                   //Tras un tiempo puede volver a crear portales
    }
    void PuedeGenerar()
    {
        generated = false;
    }
}

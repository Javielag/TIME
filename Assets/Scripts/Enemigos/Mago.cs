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
    public Animator animator;
    void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        thisEnemy = GetComponent<RangeEnemy>();
        player = GameManager.instance.GetPlayer();
    }
    void Update()
    {   //Si no hay uno creado y no se está moviendo        3 barras = Donde poner las cosas si queremos evitar que salgan los portales
        ///Añadir GameManager.instance.canTeleport aquí
        if (!generated && !thisEnemy.Moving())
        {
            ///GameManager.instance.canTeleport = false aquí
            Debug.Log("Iniciando casteamiento");
            thisEnemy.SetCanMove(false);        //Impide el movimiento
            generated = true;
            animator.SetBool("isAttacking", true); //inicia animacion de ataque
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
            animator.SetBool("isAttacking", false); //deja de atacar
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

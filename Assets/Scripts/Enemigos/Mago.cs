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
        thisEnemy = this.gameObject.GetComponent<RangeEnemy>();
        player = GameManager.instance.GetPlayer();            
    }
    void Update()
    {
        if (!generated && !thisEnemy.Moving())
        {
            Debug.Log("Iniciando casteamiento");
            thisEnemy.SetCanMove(false);
            generated = true;
            Invoke("CreaPortales", 0);
        }
    }
    void CreaPortales()
    {
        PortalMago portalOfensivo = Instantiate(PortalMagoPrefab, player.transform.position, Quaternion.identity, bulletPool);
        PortalMago portalDefensivo = Instantiate(PortalMagoPrefab, transform.position, Quaternion.identity, bulletPool);
        portalOfensivo.SetOtherPortal(portalDefensivo.transform.position);
        portalDefensivo.SetOtherPortal(portalOfensivo.transform.position);
        coroutine = ActivaPortales(portalOfensivo, portalDefensivo);
        StartCoroutine(coroutine);
        
    }
    IEnumerator ActivaPortales(PortalMago portal1, PortalMago portal2)
    {
        yield return new WaitForSeconds(tiempoCasteo);
        portal1.SetActive(true); portal2.SetActive(true);
        thisEnemy.SetCanMove(true);
        Invoke("PuedeGenerar", tiempoCasteo);
    }
    void PuedeGenerar()
    {
        generated = false;
    }
}

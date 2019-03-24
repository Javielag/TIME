using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour {
    public PortalMago PortalMagoPrefab;
    public float tiempoCasteo;
    GameObject player;
    void Start()
    {
        player = GameManager.instance.GetPlayer();
        Debug.Log("Iniciando casteamiento");
        Invoke("CreaPortales", tiempoCasteo);
    }
    void CreaPortales()
    {
        PortalMago portalAtacante = Instantiate(PortalMagoPrefab, player.transform.position, Quaternion.identity);
        PortalMago portalDefensivo = Instantiate(PortalMagoPrefab, transform.position, Quaternion.identity);
    }
}

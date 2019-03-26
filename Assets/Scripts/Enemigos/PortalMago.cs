using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMago : MonoBehaviour {
    [SerializeField]bool active = false, hasTeleported = false;
    Vector2 distance;
    Vector2 otherPortal;

    private void OnTriggerStay2D(Collider2D collision)
    {   //Si está activo y no ha realizado el teletransporte
        if (active && !hasTeleported)
        {
            Debug.Log(collision.gameObject.name);
            distance = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);   //Calcula la distancia con el personaje
            collision.transform.position = new Vector2(otherPortal.x - distance.x, otherPortal.y - distance.y);                                     //Lo pone en la posición correspondiente del otro portal
            hasTeleported = true;
            
        }
    }
    public void SetOtherPortal(Vector2 thisOne)
    {
        otherPortal = thisOne;
    }
    public void SetActive(bool state)
    {
        active = state;
    }
}

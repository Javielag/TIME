using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMago : MonoBehaviour {
    Vector2 distance;
    List<GameObject> objetos = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objetos.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objetos.Remove(collision.gameObject);
    }
    public void Teleport(Vector2 otherPortal)
    {
        foreach(GameObject things in objetos)
        {
            if (things)
            {
                distance = new Vector2(transform.position.x - things.transform.position.x, transform.position.y - things.transform.position.y);         //Calcula la distancia con el centro del portal
                things.transform.position = new Vector2(otherPortal.x - distance.x, otherPortal.y - distance.y);//Los mueve al otro portal
            }
        }
    }
}

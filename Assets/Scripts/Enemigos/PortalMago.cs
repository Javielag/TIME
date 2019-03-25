using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMago : MonoBehaviour {
    [SerializeField]bool active = false;
    Vector2 distance;

    private void OnTriggerStay2D(Collider2D collision)
    {      
        if (active)
        {
            Debug.Log(collision.gameObject.name);
            distance = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - transform.position.y);
            collision.transform.position = new Vector2();
        }
    }
}

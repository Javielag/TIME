using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMago : MonoBehaviour {
    [SerializeField]bool active = false;
    Vector2 distance;
    Vector2 otherPortal;

    private void OnTriggerStay2D(Collider2D collision)
    {      
        if (active)
        {
            Debug.Log(collision.gameObject.name);
            distance = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            collision.transform.position = new Vector2(otherPortal.x - distance.x, otherPortal.y - distance.y);
        }
    }
    public void SetOtherPortal(Vector2 thisOne)
    {
        otherPortal = thisOne;
    }
}

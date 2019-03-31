using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cientifico : MonoBehaviour {

    private void OnDestroy()
    {
        GameManager.instance.GeneraOleada();
    }
    public void Ofrece(GameObject esto)
    {
        Instantiate(esto, transform.GetChild(0).position, Quaternion.identity, transform);
    }
}

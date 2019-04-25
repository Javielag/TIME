using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour {

    public KeyCode skipKey;
    public bool yeetThroughOleadas;
    // Update is called once per frame
    void Update () {
        if (yeetThroughOleadas && Input.GetKeyDown(skipKey))
        {
            GameManager.instance.SaltaOleada();

        }
    }
}

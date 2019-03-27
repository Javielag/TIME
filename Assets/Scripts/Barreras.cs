using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreras : MonoBehaviour {

    public bool barrera, invbarrera;
    public GameObject pompa;
        
    public void DestroyPompa()
    {
        
        Destroy(pompa);
    }
}

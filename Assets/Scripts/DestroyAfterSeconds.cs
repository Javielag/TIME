using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float seconds=15f;
    private void Start()
    {
        Invoke("DestroyThis",seconds);
    }
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    public void Override(float newTime)
    {
        CancelInvoke("DestroyThis");
        Invoke("DestroyThis",newTime);
    }
}

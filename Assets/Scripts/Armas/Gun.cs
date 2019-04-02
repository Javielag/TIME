using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Weapon iAmWeapon;
    public int magSize, projectiles = 1;//tamaño de cargador y daño,cantidad de proyectiles disparados
    public float rate, range, reload, maxSpread;//velocidad de disparo, rango, tiempo de recarga,dispersion
    public bool automatic; //falso si ee semiautomatico
    private float nextShot;//ms entre disparos
    [SerializeField] private bool canShoot = true, isReloading = false;
    [SerializeField]
    private int bulletsLeft;//balas que quedan en el cargador
    public Bullet bulletPrefab;
    Transform bulletPool;
    Random rnd = new Random();
    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        nextShot = Time.time;
        bulletsLeft = magSize;
        //coge el transform de su abuelo GunPivot
    }
    private void OnEnable()
    {
        isReloading = false;              //Cuando se activa el arma, asegura que no esté recargándose y que puede disparar
        canShoot = true;
        if (GameManager.instance)        //Medida seguridad
        {
            GameManager.instance.UpdateMaxAmmo(magSize);
            GameManager.instance.UpdateAmmo(bulletsLeft);
        }        
    }
    public void Disable()
    {
        GameManager.instance.DisableWeapon(bulletsLeft,magSize);
    }
    private void Update()
    {
        if (automatic)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                if (bulletsLeft > 0)
                {
                    Shoot();
                }
                else if(!isReloading)
                {
                    StartCoroutine(Reload());
                }
                
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) && canShoot)
            {
                if (bulletsLeft > 0)
                {
                    Shoot();
                }
                else if(!isReloading)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }
    private void Shoot()
    {
        for(int shots = 0; shots < projectiles; shots++)
        {
            Vector2 direction = transform.lossyScale.x * transform.right;
            Bullet newBullet = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity, bulletPool);
            newBullet.PointAt(direction,Random.Range(-maxSpread,maxSpread));
            //le dice a la bala que se destruya tras recorrer una distancia
            newBullet.gameObject.GetComponent<DestroyAfterSeconds>().Override(range / newBullet.GetSpeed());
        }
        bulletsLeft--;
        GameManager.instance.UpdateAmmo(bulletsLeft);
        canShoot = false;
        Invoke("ResetFire", rate);
    }
    public IEnumerator Reload()
    {
        isReloading = true;                                 //Impide que el jugador recargue varias veces
        CancelInvoke("ResetFire");                          //Impide que el jugador continue disparando
        canShoot = false;
        Debug.Log(isReloading);
        yield return new WaitForSeconds(reload);            //Espera reload segundos para continuar
        bulletsLeft = magSize;
        isReloading = false;
        canShoot = true;
        GameManager.instance.UpdateAmmo(bulletsLeft);
        Debug.Log(isReloading);
        GameManager.instance.ReloadingIconUI(false);
    }
    public void ResetFire()
    {
        canShoot = true;
    }
    public bool Reloading()
    {
        return isReloading;
    }
    public bool MaxAmmo()                                   //Dice si tiene munición máxima
    {
        return (magSize == bulletsLeft);

    }
    public void CannotShoot()
    {
        CancelInvoke("ResetFire");                          //Evita que dispare si está cambiando de arma
        canShoot = false;
    }
    public void NotReload()
    {
        isReloading = false;
    }
    public void Switched()                                  //Método auxiliar para poder actualizar la munición de la UI en la secundaria
    {
        GameManager.instance.UpdateSecondaryAmmo(bulletsLeft, magSize);
    }
    public int BulletsLeft() { return bulletsLeft; }
    public void UpdateAmmo()
    {
        bulletsLeft = magSize;
    }
}
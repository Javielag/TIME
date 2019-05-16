using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speedMax = 5f, accel = 2f;//limite basico de velocidad y aceleracion
    private float xtraSpeed,maxSpeed;//tope extra y tope actual
    WeaponManager wm;
    public KeyCode melee, weaponChange, reloadKey,interact, pauseMenu, pauseMenu2;
    public float changeTime,interactRange;
    public LayerMask interactableLayer;
    Vector2 moveDir;//almacena la direccion en la que se mueve el jugador
    Rigidbody2D rb;
    public Animator anim;
    int[] weapon = new int[] {0, 1};
    // Use this for initialization
    private void Awake()
    {
        //le dice al GameManager que es el jugador  
       // GameManager.instance.SetPlayer(this.gameObject);
    }
    void Start () {
        xtraSpeed = maxSpeed=speedMax;
        rb = GetComponent<Rigidbody2D>();
        wm = GetComponentInChildren<WeaponManager>();
        StartCoroutine(wm.SwitchWeapon(0));
        StartCoroutine(wm.SwitchWeapon(0));
        wm.first = false;
        GameManager.instance.SetPlayer(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (wm.IsReloading()) maxSpeed = xtraSpeed;
        else maxSpeed = speedMax;
        //actualiza en input de movimiento
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Mathf.Abs(rb.velocity.x) > 0.5 || Mathf.Abs(rb.velocity.y) > 0.5) anim.SetBool("Moving", true);
        else anim.SetBool("Moving", false);

        //cambio de armas
        if (Input.GetKeyDown(weaponChange))  
        {
            wm.StartWeaponSwitch();
        }
        //recarga
        if (Input.GetKeyDown(reloadKey) && !wm.CheckAmmo())         //Evita recargar si tiene el cargador lleno
        {
            wm.StartReload();
        }
        //interaccion
        if (Input.GetKeyDown(interact))
        {
            //crea un area circular en la que el jugador interactúa
            Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);
            {
                foreach (Collider2D interactingWith in interactables)
                {
                    if (interactingWith != null)
                    {
                        if (interactingWith.GetComponent<WeaponPickup>()) //Si no interactúa con nada, no hace nada
                        {
                            wm.ChangeWeapon(interactingWith);
                        }
                        else if (interactingWith.GetComponent<PickUp_Vida>())
                        {
                            interactingWith.GetComponent<PickUp_Vida>().Interacted();
                        }
                        else if (interactingWith.GetComponent<PickUp_Speed>())
                        {
                            interactingWith.GetComponent<PickUp_Speed>().Interacted();
                        }
                        else if (interactingWith.GetComponent<PickUp_Cadencia>())
                        {
                           interactingWith.GetComponent<PickUp_Cadencia>().Interacted();
                        }
                        else if (interactingWith.GetComponent<PickUp_Municion>())
                        {
                            interactingWith.GetComponent<PickUp_Municion>().Interacted();
                        }
                        if (interactingWith.GetComponent<DestroyOnInteraction>())
                        {
                            Destroy(interactingWith.gameObject);
                        }
                    }
                }
            }
           
        }
        if (Input.GetKeyDown(pauseMenu) || Input.GetKeyDown(pauseMenu2))
        {
            GameManager.instance.ChangeMenuState();
        }

    }
    private void FixedUpdate(){
        if(rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDir*accel,ForceMode2D.Force);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //dibuja el area de interaccion
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
    public void setXtraSpeed(float xtra)
    {
        xtraSpeed = xtra;
    }
    public void CheatSpeed(float newSpeedMax, float newAccel)
    {
        speedMax = newSpeedMax; accel = newAccel;
    }
    public void AnimBoolFalse()
    {
        this.GetComponentInChildren<PlayerMelee>().AnimBoolFalse(); 
    }
}

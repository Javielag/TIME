using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 5f, accel = 2f;
    WeaponManager wm;
    public KeyCode melee, weaponChange, reloadKey,interact;
    public float changeTime,interactRange;
    public LayerMask interactableLayer;
    Vector2 moveDir;//almacena la direccion en la que se mueve el jugador
    Rigidbody2D rb;
    int[] weapon = new int[] {0, 1};
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        wm = GetComponentInChildren<WeaponManager>();
        StartCoroutine(wm.SwitchWeapon(0));
        StartCoroutine(wm.SwitchWeapon(0));
        wm.first = false;
        //le dice al GameManager que es el jugador
        GameManager.instance.SetPlayer(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //actualiza en input de movimiento
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

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
            Collider2D interactingWith = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);
            {
                if(interactingWith != null)
                {
                    if (interactingWith.GetComponent<WeaponPickup>()) //Si no interactúa con nada, no hace nada
                    {
                        wm.ChangeWeapon(interactingWith);
                    }
                    if (interactingWith.GetComponent<DestroyOnInteraction>())
                    {
                        Destroy(interactingWith.gameObject);
                    }
                }
            }
           
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health =5;

    

    
    // se ejecuta una sola vez al iniciar el juego
    void Start()
    {
        // es para introducir las caracteristicas de rigid body en el 
        // script
        Rigidbody2D =GetComponent<Rigidbody2D>();
        // es para introducir las caracteristicas de Animator en el 
        // script
        Animator =GetComponent<Animator>();
    }

    // se ejecuta constantemente
    void Update()
    {
        // se hace para capturar las letras que daran el movimiento del jugador
        Horizontal = Input.GetAxisRaw("Horizontal");

        // verifica si jhon va hacia la derecha o hacia la izquierda
        if (Horizontal<0) transform.localScale = new Vector3 (-1.0f, 1.0f,1.0f);
        else if (Horizontal>0) transform.localScale = new Vector3 (1.0f, 1.0f,1.0f);

        // verifica que el estado de running se de con la posicion diferente de 0 
        Animator.SetBool("Running", Horizontal != 0.0f);

        //Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.3f))
        {
            Grounded = true;
        }
        else Grounded = false;
        // lee el teclaro la letra w que es con la que vamos a saltar
        if (Input.GetKeyDown(KeyCode.W) && Grounded )
        {
            Jump();
        }

        // si el jugador presiona la tecla espacio dispara
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time; 
        }
    }

    // funcion de saltar ejerce una fuerza sobre la y
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {

        // verifica la direccion 
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.3f ,Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
    // este es porque las fisicas son mucho mas constantes
    private void FixedUpdate()
    {
        // vector dos espera las instruccions de movimiento en x y y 
        Rigidbody2D.velocity = new Vector2 (Horizontal,Rigidbody2D.velocity.y); 
    }

    
    public void Hit ()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
    
}

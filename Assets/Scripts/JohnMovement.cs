using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Image healthBar;

    public float speed;
    public float jumpForce;
    public float cooldownTime;
    public float bulletOffset;
    public float maxHealth;

    private Rigidbody2D componentRigidbody2D;
    private Animator componentAnimator;

    private float horizontal;
    private bool grounded;
    private float lastShoot;
    private float currentHealth;

    void Start()
    {
        componentRigidbody2D = GetComponent<Rigidbody2D>();
        componentAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        componentAnimator.SetBool("running", horizontal != 0.0f);

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else grounded = false;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShoot + cooldownTime)
        {
            Shoot();
            lastShoot = Time.time;
        }

        if (transform.position.y < -0.5f)
        {
            RestartScena();
        }
    }

    private void Jump()
    {
        componentRigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * bulletOffset, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        componentRigidbody2D.velocity = new Vector2(horizontal * speed, componentRigidbody2D.velocity.y);
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth = currentHealth - damageValue;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth == 0)
        {
            Destroy(gameObject, 1.0f);
            RestartScena();
        }
    }

    private void RestartScena()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}


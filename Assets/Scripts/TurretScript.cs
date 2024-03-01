using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject john;
    public GameObject bulletPrefab;
    public ScoreScript scoreScript;

    public float cooldownTime;
    public float bulletOffset;
    public float maxHealth;
    public float numberOfPoints;

    private Animator componentAnimator;

    private float lastShoot;
    private bool canShoot = true;
    private bool canMove = true;

    void Start()
    {
        componentAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = john.transform.position - transform.position;

        if (canMove)
        {
            if (direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        float distanceX = Mathf.Abs(john.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(john.transform.position.y - transform.position.y);

        if (distanceX < 1.0f && distanceY < 0.1f && canShoot && Time.time > lastShoot + cooldownTime)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * bulletOffset, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void TakeDamage(float damageValue)
    {
        maxHealth = maxHealth - 1;
        if (maxHealth == 0)
        {
            scoreScript.ScorePoints(numberOfPoints);
            canShoot = false;
            canMove = false;
            componentAnimator.SetBool("CanExplote", true);
            componentAnimator.SetBool("IsBroke", true);
        }
    }
}

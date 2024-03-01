using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GruntScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject john;
    public AudioClip sound;
    public ScoreScript scoreScript;

    public float cooldownTime;
    public float bulletOffset;
    public float maxHealth;
    public float numberOfPoints;

    private Animator componentAnimator;

    private float lastShoot;
    private bool canShoot = true;

    void Start()
    {
        componentAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 direction = john.transform.position - transform.position;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

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

        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * bulletOffset, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void TakeDamage(float damageValue)
    {
        maxHealth = maxHealth - damageValue;
        if (maxHealth == 0)
        {
            scoreScript.ScorePoints(numberOfPoints);
            componentAnimator.SetBool("IsDead", true);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
            canShoot = false;
            Destroy(gameObject, 1.0f);
        }
    }
}

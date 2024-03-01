using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemyScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject john;
    public GameObject bulletSpawnPoint;
    public GameObject deathEffectPrefab;
    public ScoreScript scoreScript;
    public AudioClip sound;

    public float fireRate;
    public float maxHealth;
    public float numberOfPoints;

    private Animator componentAnimator;

    private float nextFireTime;
    private int numberOfDeathEffects = 10;

    void Start()
    {
        componentAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            float distanceToJohnX = Mathf.Abs(john.transform.position.x - transform.position.x);
            float distanceToJohnY = Mathf.Abs(john.transform.position.y - transform.position.y);

            if (distanceToJohnY <= 1.0f && distanceToJohnX <= 0.2f)
            {
                Shoot();

                nextFireTime = Time.time + 1f / fireRate;
            }
            else
            {
                componentAnimator.SetBool("IsShooting", false);
            }
        }
    }

    private void Shoot()
    {
        componentAnimator.SetBool("IsShooting", true);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(Vector2.down);
    }

    public void TakeDamage(float damageValue)
    {
        maxHealth = maxHealth - damageValue;
        if (maxHealth == 0)
        {
            scoreScript.ScorePoints(numberOfPoints);
            componentAnimator.SetBool("IsDead", true);
            SpawnDeathEffects();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject, 0.5f);
        }
    }

    private void SpawnDeathEffects()
    {
        for (int i = 0; i < numberOfDeathEffects; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 2f;
            Instantiate(deathEffectPrefab, randomPosition, Quaternion.identity);
        }
    }
}
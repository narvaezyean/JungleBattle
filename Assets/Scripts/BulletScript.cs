using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip sound;

    public float speed;
    public float damageValue;

    private Rigidbody2D componentRigidbody2D;
    private Vector2 directionBullet;

    void Start()
    {
        componentRigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    private void FixedUpdate()
    {
        componentRigidbody2D.velocity = directionBullet * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        directionBullet = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        JohnMovement john = other.GetComponent<JohnMovement>();
        GruntScript grunt = other.GetComponent<GruntScript>();
        TurretScript turret = other.GetComponent<TurretScript>();
        BossEnemyScript boss = other.GetComponent<BossEnemyScript>();

        if (john != null)
        {
            john.TakeDamage(damageValue);
        }

        if (grunt != null)
        {
            grunt.TakeDamage(damageValue);
        }

        if (turret != null)
        {
            turret.TakeDamage(damageValue);
        }

        if (boss != null)
        {
            boss.TakeDamage(damageValue);
        }

        DestroyBullet();
    }
}


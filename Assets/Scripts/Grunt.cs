using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Jhon;
    private float LastShoot;
    private int Health =3;
    
    private void Update()
    {
        if ( Jhon == null)return;
        // se hace para que grunt este siempre mirando la posicion de jhon y lo pueda seguir
        Vector3 direction = Jhon.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3 (-1.0f,1.0f, 1.0f);

        float distance = Mathf.Abs(Jhon.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }
        
    private void Shoot()
    {
            
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.3f ,Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

     public void Hit ()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }

   
    
}

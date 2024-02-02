using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject John;

    private float LastShoot;
    private int Health = 3;

    void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);
        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
            Vector3 direction;
            if (transform.localScale.x == 1.0f) direction = Vector3.right;
            else direction = Vector3.left;

            // Al invocar la función, se modifica el prefab de Bullet para disparar.
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
     }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}

//Creamos el vector direccion
//Transform position es la posicion del enemigo
//John transform position es la posicion de John
//la posicion de John - la posicion del enemigo, obtenemos el vector3 direction que va del enemigo a john
//Si este vector direccion su componente x que va de izquierda a derecha es positivo.
//Utilizamos el transform.localScale: (1, 1, 1) significa que el objeto no ha sido escalado y tiene su tamaño original en cada eje.  (X, Y, Z)

//Disparo de Grunt
//Creamos Variable Float - distance
//Si John esta en la posicion 5 y grunt esta en la posicion 3, 5-3 = 2 estan a 2 de distancia
//Problema si john esta en la posicion 3 y Grunt en la 5 va dar -2
//Usamos Mathf.Abs para que siempre de 2 en positivo, realizamos valor absoluto con esa funcion para que de en positivo.
//Si la distancia es menor a un metro pues que dispare.
//Llamamos la funcion Shoot
//Creamos un Delay - Creamos variable Private float Last Shoot como hicimos con John

//Funcion Debug, que vamos a utilizar para debugear para encontrar bugs Debug.Log(Shoot); luego abrimos la consola
//Una vez comprobamos que la funcion shoot funciona realizamos copy paste de la misma funcion que tiene Jhon
//Creamos variable publica para el PreFab de la bala
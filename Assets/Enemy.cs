using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    public bool isHit = false;

    public float speed = 2f; // Velocidad de movimiento del enemigo
    public Transform player; // Referencia al transform del jugador

    void Update()
    {
        FollowPlayer(); // Llamada a la función para seguir al jugador
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        isHit = true;

        if (health <= 0)
        {
            Die();
        }

        Invoke("ResetHitState", 0.1f);
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void ResetHitState()
    {
        isHit = false;
    }

    void FollowPlayer()
    {
        if (player == null) return; // Asegurarse de que el jugador esté asignado

        // Calcular la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador
        transform.position += direction * speed * Time.deltaTime;
    }
}

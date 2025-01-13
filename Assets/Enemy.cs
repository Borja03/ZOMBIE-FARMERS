using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public bool isHit = false;
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public Transform player; // Referencia al transform del jugador
    public GameObject coinPrefab; // Prefab de la moneda

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
        // Instanciar la moneda en la posición actual del enemigo
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity); // Crea la moneda en la posición del enemigo
        }

        Destroy(gameObject); // Destruir el enemigo
    }

    void ResetHitState()
    {
        isHit = false;
    }

    void FollowPlayer()
    {
        if (player == null) return; // Asegurarse de que el jugador esté asignado

        // Calcular la dirección hacia la posición actual del jugador en 2D
        Vector2 direction = (player.position - transform.position).normalized;

        // Mover al enemigo hacia la posición del jugador en 2D
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}

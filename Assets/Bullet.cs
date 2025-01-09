using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed; // Corregí 'linearVelocity' a 'velocity'
        Destroy(gameObject, 3f); // Destruir el objeto después de 3 segundos
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject); // Destruir la bala al impactar
    }
}

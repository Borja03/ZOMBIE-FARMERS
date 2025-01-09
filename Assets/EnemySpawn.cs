using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a instanciar
    public float spawnInterval = 2f; // Intervalo entre spawns
    public int maxEnemies = 10; // Número máximo de enemigos en la escena
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Tamaño del área donde se generan los enemigos

    private int currentEnemyCount = 0; // Contador de enemigos activos

    void Start()
    {
        // Iniciar el proceso de generación de enemigos
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

   void SpawnEnemy()
{
    if (currentEnemyCount >= maxEnemies || enemyPrefab == null)
        return; // Detener si no hay prefab asignado o se alcanzó el límite

    // Generar una posición aleatoria dentro del área definida
    Vector2 randomPosition = new Vector2(
        Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
        Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
    );

    // Convertir la posición al mundo (añadiendo la posición del GameObject)
    Vector3 spawnPosition = transform.position + (Vector3)randomPosition;

    // Instanciar el enemigo en la posición calculada
    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    // Incrementar el contador de enemigos
    currentEnemyCount++;

    // Opción: Añadir una verificación para destruir enemigos huérfanos
    if (enemy == null)
    {
        Debug.LogWarning("Instantiated enemy is null. Check your prefab.");
    }
}

    // Método para reducir el contador cuando un enemigo sea eliminado
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}

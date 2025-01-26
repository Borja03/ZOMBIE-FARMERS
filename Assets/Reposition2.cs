using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition2 : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento
    public float maxDistance = 10f; // Distancia máxima permitida desde (0,0)
    public float changeDirectionInterval = 2f; // Tiempo entre cambios de dirección

    private Vector3 targetDirection; // Dirección objetivo
    private Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void Start()
    {
        // Inicia un movimiento aleatorio
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        // Movimiento continuo en la dirección objetivo
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        // Verifica si está fuera del límite y corrige
        if (Vector3.Distance(Vector3.zero, transform.position) > maxDistance)
        {
            Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;
            targetDirection = directionToCenter; // Regresa hacia el centro
        }
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            // Genera una nueva dirección aleatoria
            targetDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            // Espera antes de cambiar la dirección nuevamente
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))  
            return;

        // Si colisiona con algo que no sea el área, ignora
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);        
        float diffY = Mathf.Abs(playerPos.y - myPos.y); 

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;  
        float dirY = playerDir.y < 0 ? -1 : 1;  

        switch (transform.tag)
        {
            case "Ground":
                if(diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);    
                }else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);    
                }
                break;
            case "Enemy":
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); 
                }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    SpriteRenderer spriter;
    int WeaponNumber;   //0~5
    int WeaponLevel;    //MaxLevel = 6

    public float radius = 2f;  // Radio del círculo en el que el arma puede moverse

    public Transform player;   // Transform del jugador (asignado en el Inspector o desde código)

    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        // Si el player no está asignado, intentamos buscarlo
        if (player == null)
        {
            player = GameManager.instance.player.transform;
        }
    }

    void LateUpdate()
    {
        // Aseguramos que el arma esté siempre en una posición relativa al jugador
        Vector3 playerPosition = player.position;

        // Rotar el arma hacia el ratón
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Asegurarse de que la posición Z del ratón sea 0 (2D)

        Vector3 direction = mousePosition - playerPosition; // Dirección desde el jugador al ratón
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // Convertir la dirección a ángulo
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Rotar el arma hacia el ratón

        // Colocar el arma a una distancia fija (por ejemplo, dentro del radio definido)
        Vector3 weaponPosition = playerPosition + direction.normalized * radius;

        transform.position = weaponPosition; // Coloca el arma en su posición relativa al jugador
    }

    void Weapon0()
    {

    }
}

using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform player;      // Referencia al jugador
    public Transform weapon;      // Referencia al arma
    public float radius = 3f;     // Radio del círculo alrededor del jugador

    void Update()
    {
        // 1. Obtén la posición del ratón en el mundo (asegúrate de que la cámara es la principal y esté configurada para 2D)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Asegúrate de que la posición Z esté a 0 para trabajar en 2D

        // 2. Calcula el vector entre el jugador y el ratón
        Vector3 direction = mousePos - player.position;

        // 3. Normaliza el vector para obtener una dirección, pero no la distancia
        direction.Normalize();

        // 4. Calcula el ángulo entre el jugador y el ratón
        float angle = Mathf.Atan2(direction.y, direction.x);

        // 5. Posiciona el arma en el círculo alrededor del jugador en la posición correspondiente al ángulo
        Vector3 weaponPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        // 6. Mueve el arma a la nueva posición
        weapon.position = weaponPos;

        // 7. Haz que el arma apunte al ratón
        Vector3 lookDir = mousePos - weapon.position;
        float weaponAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(new Vector3(0, 0, weaponAngle));
    }
}

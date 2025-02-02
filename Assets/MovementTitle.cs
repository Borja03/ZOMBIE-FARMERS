using UnityEngine;

public class MovementTitle : MonoBehaviour
{
    // Parámetros ajustables desde el Inspector:
    [Tooltip("Intensidad del movimiento (cuanto mayor, mayor desplazamiento).")]
    public float vibrationAmplitude = 0.05f;
    [Tooltip("Velocidad del movimiento (cuanto mayor, más rápido vibra).")]
    public float vibrationFrequency = 20f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Calcula un desplazamiento circular usando seno y coseno.
        float offsetX = Mathf.Sin(Time.time * vibrationFrequency) * vibrationAmplitude;
        float offsetY = Mathf.Cos(Time.time * vibrationFrequency) * vibrationAmplitude;
        Vector3 offset = new Vector3(offsetX, offsetY, 0f);

        transform.localPosition = initialPosition + offset;
    }
}

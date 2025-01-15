using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Text countdownText; // Asocia este componente en el Inspector
    private float timeRemaining = 60f; // Duración de 1 minuto
    private bool isCountingDown = true;
    public SubirNivel subirNivel; // Referencia a la clase SubirNivel

    void Update()
    {
        if (isCountingDown)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownDisplay(Mathf.CeilToInt(timeRemaining));
            }
            else
            {
                isCountingDown = false;
                timeRemaining = 0;
                UpdateCountdownDisplay(0);
                OnCountdownFinished();
            }
        }
    }

    private void UpdateCountdownDisplay(int seconds)
    {
        countdownText.text = seconds.ToString();
    }

    private void OnCountdownFinished()
    {
        Debug.Log("¡Cuenta regresiva finalizada!");
        if (subirNivel != null)
        {
            subirNivel.IncrementarNivel(); // Llama al método para incrementar el nivel
        }
        // Aquí puedes reiniciar el temporizador si es necesario o detenerlo.
        timeRemaining = 60f; // O puedes configurarlo en 60 nuevamente si quieres reiniciar
        isCountingDown = true;
    }
}

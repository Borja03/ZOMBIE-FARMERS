using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Text countdownText; // Asocia este componente en el Inspector
    private float timeRemaining = 60f; // Duración de 1 minuto
    private bool isCountingDown = true;

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
        // Aquí puedes agregar lógica adicional para cuando el countdown termine.
    }
}

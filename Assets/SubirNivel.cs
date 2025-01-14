using UnityEngine;
using UnityEngine.UI;

public class SubirNivel : MonoBehaviour
{
    public Text levelText; // El texto donde se mostrará el nivel
    private int level = 0; // Contador de niveles

    // Método para aumentar el nivel
    public void IncrementarNivel()
    {
        level++;
        levelText.text = "Lv." + level.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Método para detectar si el jugador ha colisionado con la puerta
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si el jugador ha recogido la llave a través de GameManager
            if (GameManager.instance.HasKey())
            {
                Debug.Log("¡Has escapado del laberinto!");
                EndGame();
                // Cargar la siguiente escena
                // SceneManager.LoadScene("Escena"); 
            }
            else
            {
                Debug.Log("Necesitas la llave para salir.");
            }
        }
    }

    void EndGame()
    {
        // Reiniciar la escena actual
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // cerrar el juego
        Application.Quit();
    }
}

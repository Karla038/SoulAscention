using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si el jugador ha recogido todas las llaves necesarias para el nivel actual
            if (GameManager.instance.HasCollectedAllKeys())
            {
                Debug.Log("¡Has escapado del laberinto!");
                CargarSiguienteEscena();
            }
            else
            {
                Debug.Log("Necesitas todas las llaves para salir.");
            }
        }
    }

    void CargarSiguienteEscena()
    {
        // Cargar la siguiente escena en base al nivel actual
        int nextLevel = GameManager.instance.currentLevel + 1;
        string nextSceneName = "Level" + nextLevel; // Asegúrate de que los nombres de las escenas sigan este formato

        // Verificar si la siguiente escena existe en los Build Settings
        if (SceneExists(nextSceneName))
        {
            // Avanzar al siguiente nivel en el GameManager
            GameManager.instance.AdvanceLevel();
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("¡Felicidades! Has completado todos los niveles.");
            // Aquí puedes agregar lógica adicional, como mostrar una pantalla de victoria o reiniciar el juego.
        }
    }

    // Método para verificar si una escena está incluida en los Build Settings
    bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameInBuildSettings == sceneName)
            {
                return true;
            }
        }
        return false;
    }

}

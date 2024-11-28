using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public VidaJugador vidaJugador;

    public int vidas = 3;

    private bool hasKey = false; // Estado de la llave

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
        }
        else
        {
            Debug.Log("Mas de un gameMagnament en escena");
        }
    }

    // Función para actualizar el estado de la llave
    public void SetHasKey(bool value)
    {
        hasKey = value;
    }

    // Función para verificar si el jugador tiene la llave
    public bool HasKey()
    {
        return hasKey;
    }

    public void PerderVida(){
        vidas -=1;
        if(vidas >= 0){
            vidaJugador.DesactivarVida(vidas);
        }else {
            EndGame();
        }
        
    }

    void EndGame()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

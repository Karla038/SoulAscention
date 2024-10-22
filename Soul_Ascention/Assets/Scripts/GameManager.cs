using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool hasKey = false; // Estado de la llave

    void Awake()
    {
        // Asegurarse de que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
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
}

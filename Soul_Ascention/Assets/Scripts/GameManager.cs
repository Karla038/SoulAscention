using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0; // Puntuación inicial
    public Text scoreText;
    public int currentLevel = 1; // Nivel actual
    private Dictionary<int, int> keysRequiredPerLevel = new Dictionary<int, int>
    {
        {1, 1}, // Nivel 1 requiere 1 llave
        {2, 2}, // Nivel 2 requiere 2 llaves
        {3, 3}  // Nivel 3 requiere 3 llaves
    };
    private int keysCollected = 0; // Contador de llaves recogidas en el nivel actual

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // Método para añadir puntuación
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Método para actualizar el texto de puntuación en la UI
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Método para recolectar una llave y actualizar el contador
    public void CollectKey()
    {
        keysCollected++;
        Debug.Log("Llaves recogidas en nivel " + currentLevel + ": " + keysCollected);
    }

    // Método para verificar si el jugador ha recogido todas las llaves necesarias
    public bool HasCollectedAllKeys()
    {
        return keysCollected >= keysRequiredPerLevel[currentLevel];
    }

    // Método para pasar al siguiente nivel, reseteando el contador de llaves
    public void AdvanceLevel()
    {
        keysCollected = 0;
        currentLevel++;
    }
}

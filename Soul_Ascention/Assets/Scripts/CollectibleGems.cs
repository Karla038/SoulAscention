using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGems : MonoBehaviour
{
    public int scoreValue = 10; // Puntos que esta gema añade al ser recogida

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamar a AddScore en el GameManager y pasar el valor de scoreValue
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("Gema recogida! Puntaje añadido: " + scoreValue);

            // Destruir la gema para simular que ha sido recogida
            Destroy(gameObject);
        }
    }
}

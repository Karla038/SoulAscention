using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetHasKey(true); // Actualizar estado en GameManager
            Debug.Log("Llave recogida!");
            Destroy(gameObject); // Destruir la llave
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar o reiniciar escenas

public class EnemyController : MonoBehaviour
{
    //public static GameManager instance;
    public float speed = 2f; // Velocidad de movimiento
    public Transform player; // Referencia al jugador
    public LayerMask wallLayer; // Capa de las paredes del laberinto
    public float detectionRange = 5f; // Rango de detección del jugador

    private Vector2 direction; // Dirección en la que el enemigo se moverá
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetRandomDirection(); // Establecer una dirección inicial aleatoria
    }

    // Update is called once per frame
    void Update()
    {
        //Move(); // Mover al enemigo en la dirección actual
        DetectPlayer(); // Verificar si el jugador está dentro del rango
    }

    void Move()
    {
        // Mover al enemigo en la dirección seleccionada
        rb.velocity = direction * speed;

        // Comprobar si hay una pared en la dirección de movimiento
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayer);

        if (hit.collider != null)
        {
            // Si se detecta una pared, cambiar la dirección
            SetRandomDirection();
        }
    }

    void SetRandomDirection()
    {
        // Seleccionar una dirección aleatoria: arriba, abajo, izquierda o derecha
        int randomDir = Random.Range(0, 4);

        switch (randomDir)
        {
            case 0: direction = Vector2.up; break;       // Arriba
            case 1: direction = Vector2.down; break;     // Abajo
            case 2: direction = Vector2.left; break;     // Izquierda
            case 3: direction = Vector2.right; break;    // Derecha
        }
    }

    void DetectPlayer()
    {
        // Verificar si el jugador está dentro del rango de detección
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            direction = (player.position - transform.position).normalized;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
        }
        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            SetRandomDirection();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (GameManager.instance.vidas < 0)
            {
                Debug.Log("Fin del juego.");
                EndGame();
            }else {
                GameManager.instance.PerderVida();
                Debug.Log("El enemigo te atrapó.");
            }
        }*/

    }

    
}

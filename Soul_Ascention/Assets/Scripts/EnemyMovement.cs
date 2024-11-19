using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public Transform player; // Referencia al jugador
    public LayerMask wallLayer; // Capa de las paredes del laberinto
    public float detectionRange = 5f; // Rango de detección del jugador

    private Vector2 direction; // Dirección en la que el enemigo se moverá
    private Rigidbody2D rb;
    private bool isChasingPlayer = false;
    private Animator animator;

    private float stuckTimer = 0f; // Temporizador para detectar atascos
    private float stuckDuration = 1f; // Duración máxima en la misma posición antes de cambiar dirección
    private Vector2 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetRandomDirection(); // Establecer una dirección inicial aleatoria
    }

    void Update()
    {
        DetectPlayer(); // Verificar si el jugador está dentro del rango
        Move(); // Mover al enemigo en la dirección actual
    }

    void UpdateAnimationDirection()
    {
        // Resetea todos los parámetros de animación
        animator.SetBool("isWalkingFront", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);
        animator.SetBool("isWalkingBack", false);

        // Activa el parámetro adecuado en función de la dirección
        if (Mathf.Abs(direction.x) < 0.1f && direction.y > 0) // Movimiento hacia arriba
        {
            animator.SetBool("isWalkingBack", true);
        }
        else if (Mathf.Abs(direction.x) < 0.1f && direction.y < 0) // Movimiento hacia abajo
        {
            animator.SetBool("isWalkingFront", true);
        }
        else if (direction.x < 0 && Mathf.Abs(direction.y) < 0.1f) // Movimiento hacia la izquierda
        {
            animator.SetBool("isWalkingLeft", true);
        }
        else if (direction.x > 0 && Mathf.Abs(direction.y) < 0.1f) // Movimiento hacia la derecha
        {
            animator.SetBool("isWalkingRight", true);
        }
    }

    void Move()
    {
        // Mover al enemigo en la dirección seleccionada
        rb.velocity = direction * speed;

        // Comprobar si hay una pared en la dirección de movimiento
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, wallLayer);

        if (hit.collider != null)
        {
            // Si se detecta una pared, intentar elegir una nueva dirección que no esté bloqueada
            FindNewDirection();
        }

        // Verificar si el enemigo está atascado
        if (Vector2.Distance(lastPosition, rb.position) < 0.01f) // Si apenas se ha movido
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer > stuckDuration)
            {
                // Cambiar de dirección si está atascado demasiado tiempo
                FindNewDirection();
                stuckTimer = 0f; // Reiniciar el temporizador
            }
        }
        else
        {
            stuckTimer = 0f; // Reiniciar el temporizador si se está moviendo
        }

        lastPosition = rb.position;
        UpdateAnimationDirection();
    }

    void FindNewDirection()
    {
        int attempts = 0;
        bool foundDirection = false;

        // Intentar hasta 4 veces encontrar una dirección sin obstáculos
        while (attempts < 4 && !foundDirection)
        {
            if (isChasingPlayer)
            {
                // Si está persiguiendo al jugador, ajusta la dirección hacia el jugador
                direction = (player.position - transform.position).normalized;
            }
            else
            {
                SetRandomDirection();
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, wallLayer);

            if (hit.collider == null)
            {
                // Si la dirección actual no tiene una pared, usar esa dirección
                foundDirection = true;
            }
            attempts++;
        }

        // Si después de 4 intentos no encuentra dirección, elige una dirección opuesta
        if (!foundDirection)
        {
            direction = -direction; // Invertir la dirección si no encuentra una libre
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
            // Persigue al jugador si está dentro del rango de detección
            isChasingPlayer = true;
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            // Si el jugador está fuera del rango, vuelve a moverse aleatoriamente
            isChasingPlayer = false;
            SetRandomDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Si colisiona con una pared, intenta cambiar la dirección
            FindNewDirection();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El enemigo te atrapó. Fin del juego.");
            EndGame();
        }
    }

    void EndGame()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

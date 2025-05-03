using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salut màxima del jugador
    public int currentHealth;  // Salut actual del jugador

    private GameManager gameManager; // Referència al Singleton GameManager

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager

        // Si ja hi ha un valor guardat per vidaJugador, assignem a currentHealth aquest valor.
        if (gameManager != null && gameManager.vidaJugador > 0)
        {
            currentHealth = gameManager.vidaJugador;
        }
        else
        {
            currentHealth = maxHealth; // Si no, inicialitzem amb la salut màxima
            if (gameManager != null)
            {
                gameManager.vidaJugador = maxHealth; // Guardem la salut màxima al GameManager
            }
        }

        UpdateHealthSlider(); // Actualitzem la barra de salut
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();

        if (gameManager != null)
        {
            gameManager.vidaJugador = currentHealth; // Actualitzem el valor de vidaJugador al GameManager
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("El jugador ha mort.");
        GameManager.Instance.GameOver();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();

        if (gameManager != null)
        {
            gameManager.vidaJugador = currentHealth; // Actualitzem el valor de vidaJugador al GameManager
        }
    }

    private void UpdateHealthSlider()
    {
        if (gameManager != null && gameManager.healthSlider != null)
        {
            gameManager.healthSlider.value = (float)currentHealth / maxHealth; // Actualitza la barra de salut
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aquesta classe gestiona la vida del jugador: Salut M�xima, Salut Actual, Prendre Mal i Curaci�
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salut m�xima del jugador
    public int currentHealth;  // Salut actual del jugador

    private GameManager gameManager; // Refer�ncia al Singleton GameManager

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la inst�ncia del GameManager

        // Si ja hi ha un valor guardat per vidaJugador, assignem a currentHealth aquest valor.
        if (gameManager != null && gameManager.vidaJugador > 0)
        {
            currentHealth = gameManager.vidaJugador;
        }
        else
        {
            currentHealth = maxHealth; // Si no, inicialitzem amb la salut m�xima
            if (gameManager != null)
            {
                gameManager.vidaJugador = maxHealth; // Guardem la salut m�xima al GameManager
            }
        }

        UpdateHealthSlider(); // Actualitzem la barra de salut
    }
   


    // M�tode que gestiona la p�rdua de vida quan el jugador pren mal
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Actualitzem vida actual restant el valor rebut pel par�metre amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();
        GameManager.Instance.damageFlash.TriggerFlash();

        if (gameManager != null)
        {
            gameManager.vidaJugador = currentHealth; // Actualitzem el valor de vidaJugador al GameManager
        }

        if (currentHealth <= 0)
        {
            Dead(); // Cridem m�tode Dead si la vida �s igual o menor que 0
        }

        
    }

    // M�tode que gestiona la mort del Player
    public void Dead()
    {
        Debug.Log("El jugador ha mort.");
        GameManager.Instance.GameOver(); // Cridem al m�tode GameOver a trav�s de la inst�ncia del GameManager
    }

    // M�tode que gestiona l'augment de vida quan el jugador es cura
    public void Heal(int amount)
    {
        currentHealth += amount; // Actualitzem vida actual sumant el valor rebut pel par�metre amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();

        if (gameManager != null)
        {
            gameManager.vidaJugador = currentHealth; // Actualitzem el valor de vidaJugador al GameManager
        }
    }

    // M�tode per gestionar la barra de salut
    private void UpdateHealthSlider()
    {
        if (gameManager != null && gameManager.healthSlider != null)
        {
            gameManager.healthSlider.value = (float)currentHealth / maxHealth; // Actualitza la barra de salut
        }
    }
}

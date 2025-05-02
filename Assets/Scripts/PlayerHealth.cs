using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salut m�xima del jugador
    public int currentHealth;    // Salut actual del jugador

    private GameManager gameManager; // Refer�ncia al Singleton GameManager

    private void Start()
    {
        currentHealth = maxHealth; // Inicialitza la salut actual a la m�xima
        gameManager = GameManager.Instance; // Obtenim la inst�ncia del GameManager
        UpdateHealthSlider(); // Actualitzem el Slider quan comen�a el joc
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Redu�m la salut
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitem que la salut sigui negativa
        UpdateHealthSlider(); // Actualitzem la barra de salut

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("El jugador ha mort.");
        GameManager.Instance.GameOver(); // Canvia a escena GameOver
    }

    public void Heal(int amount)
    {
        currentHealth += amount; // Augmentem la salut
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitem que la salut excedeixi el m�xim
        UpdateHealthSlider(); // Actualitzem la barra de salut
    }

    private void UpdateHealthSlider()
    {
        if (gameManager != null && gameManager.healthSlider != null)
        {
            // Actualitzem el valor del Slider en funci� de la salut actual
            gameManager.healthSlider.value = (float)currentHealth / maxHealth;
        }
    }
}

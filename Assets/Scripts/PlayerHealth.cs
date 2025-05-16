using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aquesta classe gestiona la vida del jugador: Salut M�xima, Salut Actual, Prendre Mal i Curaci�
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salut m�xima del jugador
    public int currentHealth;  // Salut actual del jugador

    private GameManager gameManager; // Refer�ncia al Singleton GameManager

    public CanvasGroup damageFlashCanvas; // CanvasGroup del UI dany
    public float flashDuration = 0.1f;    // Temps entre parpeig (modificable a l'Inspector)
    public int flashCount = 3;           // Vegades que es reprodueix el parpeig (modificable a l'Inspector)

    private Coroutine flashCoroutine;    // Evita superposicions de parpeig


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
   


    private IEnumerator FlashDamage()
    {
        for (int i = 0; i < flashCount; i++)
        {
            damageFlashCanvas.alpha = 1f; // Mostrar Parpeig al canvas
            yield return new WaitForSeconds(flashDuration);
            damageFlashCanvas.alpha = 0f; // Amagar parpeig al canvas
            yield return new WaitForSeconds(flashDuration);
        }
    }


    // M�tode que gestiona la p�rdua de vida quan el jugador pren mal
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Actualitzem vida actual restant el valor rebut pel par�metre amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();

        if (gameManager != null)
        {
            gameManager.vidaJugador = currentHealth; // Actualitzem el valor de vidaJugador al GameManager
        }

        if (currentHealth <= 0)
        {
            Dead(); // Cridem m�tode Dead si la vida �s igual o menor que 0
        }

        if (damageFlashCanvas != null)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine); // Evita que hi hagin parpeig simultani entre diferents enemics
            }
            flashCoroutine = StartCoroutine(FlashDamage());
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

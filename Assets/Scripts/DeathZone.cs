using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Aquesta classe defineix una "zona de mort" que fa que el jugador mori immediatament quan hi entra
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprova si l'objecte que ha entrat t� el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtenim el component PlayerHealth del jugador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Estructura de selecci� en funci� de si el jugador t� el component PlayerHealth. Entra quan no �s null
            if (playerHealth != null)
            {
                // Fa mal al jugador amb una quantitat igual a la seva vida actual, per el jugador mor directament
                playerHealth.TakeDamage(playerHealth.currentHealth); 
                Debug.Log("El jugador ha caigut a la zona de mort.");
            }
        }
    }
}

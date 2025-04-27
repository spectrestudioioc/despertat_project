using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager; // Referència a un component GameManager
    public bool areaEnemic;
    public enum TipusEnemic
    {
        EnemicOmbra,
        EnemicRialla,
        EnemicGranPena
    }

    public TipusEnemic tipusEnemic;
    public int dany;

    private PlayerHealth playerHealth; // Referencia a un component PlayerHealth
    

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager
            
        // Obtenim la referència al component PlayerHealth
        playerHealth = FindObjectOfType<PlayerHealth>();

        switch (tipusEnemic)
        {
            case TipusEnemic.EnemicOmbra:
                dany = 10;
                break;
            case TipusEnemic.EnemicRialla:
                dany = 15;
                break;
            case TipusEnemic.EnemicGranPena:
                dany = 20;
                break;
            default:
                break;
        }
    }


    public void TakeDamage(EnemyCollider enemyCollider)
    {
        
        if (playerHealth != null)
        {

            playerHealth.TakeDamage(dany); // Restem la vida al jugador
            // Mostrem un missatge a la consola cada vegada que apliquem dany
            Debug.Log($"S'ha aplicat {dany} de dany al jugador. Salut restant: {playerHealth.currentHealth}");
        }
    }


}

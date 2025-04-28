using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager; // Refer�ncia a un component GameManager
    public bool areaEnemic;
    public enum TipusEnemic
    {
        EnemicOmbra,
        EnemicRialla,
        EnemicGranPena
    }

    public TipusEnemic tipusEnemic;
    public int dany;

    public string paraulaClau;

    private PlayerHealth playerHealth; // Referencia a un component PlayerHealth
    

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la inst�ncia del GameManager
            
        // Obtenim la refer�ncia al component PlayerHealth
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

    public void ComprovarParaulaClau(string userInput)
    {
        if (userInput.Equals(paraulaClau, StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Paraula clau correcta!");
            ParaulaCorrecta(); // Aqu� es crida el m�tode si s'ha encertat la paraula
        }
        else
        {
            Debug.Log("Paraula clau incorrecta!");
            ParaulaIncorrecta();
        }
    }

    public void ParaulaCorrecta()
    {

    }

    public void ParaulaIncorrecta()
    {

    }


}

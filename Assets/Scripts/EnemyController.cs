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
    private Loot loot; // Referencia a un component Loot

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager
        areaEnemic = false;

        loot = GetComponent<Loot>(); // Inicialitzem la referència al component Loot

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha entrat al trigger de l'enemic!");
            
            areaEnemic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha sortit del trigger del pickup!");
            
            areaEnemic = false;
        }
    }

    private void Update()
    {
        if (areaEnemic && Input.GetKeyDown(KeyCode.E)) // Comprovem si el jugador està dins del trigger i prem la tecla E
        {
            Debug.Log("El jugador ha clicat la tecla E davant de l'Enemic");
            loot.AddToPlayerInventory();

        }
    }

}

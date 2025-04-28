using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class EnemyCollider : MonoBehaviour
{
    private GameManager gameManager; // Refer�ncia a un component GameManager
    private EnemyController enemyController; // Refer�ncia a un component EnemyController
    private bool areaEnemy;
    private Loot loot; // Referencia a un component Loot

    private float damageTimer; // Temporitzador per controlar quan aplicar el dany
    private float damageInterval; // Interval en segons per calcular quan aplicar dany

    private void Start()
    {
        // Obtenim la refer�ncia al component EnemyController de l'objecte pare i l'assignem a enemyController
        enemyController = GetComponentInParent<EnemyController>();
        gameManager = GameManager.Instance; // Obtenim la inst�ncia del GameManager
        loot = GetComponentInParent<Loot>(); // Inicialitzem la refer�ncia al component Loot
        areaEnemy = false;
        damageTimer = 0f;
        damageInterval = 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyController != null)
        {
            // Accedim als valors del tipus d'enemic i dany des del component EnemyController
            Debug.Log($"Has entrat en contacte amb {enemyController.tipusEnemic}. Et far� {enemyController.dany} de mal.");
            gameManager.MostraEnemicText(enemyController);
            areaEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && enemyController != null)
        {
            // Accedim als valors del tipus d'enemic i dany des del component EnemyController
            Debug.Log($"Has sortit de l'�rea de contacte amb {enemyController.tipusEnemic}.");
            gameManager.AmagaEnemicText(enemyController);
            areaEnemy = false;
            damageTimer = 0f; // Reiniciem el temporitzador quan el jugador surt del trigger
        }
    }

    private void Update()
    {
        if (areaEnemy)
        {
            damageTimer += Time.deltaTime; // Sumem el temps transcorregut quan entrem al trigger de l'enemic i areaEnemy �s true
            if (damageTimer >= damageInterval)
            {
                /* 
                 * Si ha passat el temps definit a damageInterval,
                 * passem la refer�ncia a EnemyController per aplicar dany
                 */                 
                enemyController.TakeDamage(this);
                damageTimer = 0f; // Reiniciem el comptador de temps per aplicar dany
            }
                
        }

        if (areaEnemy && Input.GetKeyDown(KeyCode.E)) // Comprovem si el jugador est� dins del trigger i prem la tecla E
        {
            Debug.Log("El jugador ha clicat la tecla E davant de l'Enemic");
            gameManager.MostraEnemicInputField(enemyController);
            //loot.AddToPlayerInventory();

        }
    }
}

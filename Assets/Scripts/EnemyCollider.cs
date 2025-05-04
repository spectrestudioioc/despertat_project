using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

// Aquesta classe gestiona els triggers que fa el Player amb el detectionCollider dels enemics
public class EnemyCollider : MonoBehaviour
{
    private GameManager gameManager; // Refer�ncia a un component GameManager
    private EnemyController enemyController; // Refer�ncia a un component EnemyController
    private bool areaEnemy;
    

    private float damageTimer; // Temporitzador per controlar quan aplicar el dany
    private float damageInterval; // Interval en segons per calcular quan aplicar dany

    private void Start()
    {
        // Obtenim la refer�ncia al component EnemyController de l'objecte pare i l'assignem a enemyController
        enemyController = GetComponentInParent<EnemyController>();
        gameManager = GameManager.Instance; // Obtenim la inst�ncia del GameManager
        areaEnemy = false;

        damageTimer = 0f; // Inicialitzem el comptador de temps
        damageInterval = 3f; // Definim l'interval de temps per aplicar dany
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyController != null)
        {
            // Accedim als valors del tipus d'enemic i dany des del component EnemyController
            Debug.Log($"Has entrat en contacte amb {enemyController.tipusEnemic}. Et far� {enemyController.dany} de mal.");
            areaEnemy = true; // Marquem que el jugador est� dins l'�rea
            if (enemyController.CompareTag("Immortal")) 
            {
                return; // No fa res si l'enemic �s immortal
            }
            else
            {
                // Mostrem el text que mostra que podem interactuar amb l'enemic
                gameManager.MostraEnemicText(enemyController);
                
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && enemyController != null)
        {
            // Accedim als valors del tipus d'enemic i dany des del component EnemyController
            Debug.Log($"Has sortit de l'�rea de contacte amb {enemyController.tipusEnemic}.");
            gameManager.AmagaEnemicText(enemyController);
            areaEnemy = false; // Marquem que el jugador est� fora de l'�rea
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

        // Comprovem si el jugador est� dins del trigger, no t� el TAG Immortal i prem la tecla E
        if (areaEnemy && Input.GetKeyDown(KeyCode.E) && !enemyController.CompareTag("Immortal")) 
        {
            Debug.Log("El jugador ha clicat la tecla E davant de l'Enemic");
            gameManager.MostraEnemicInputField(enemyController); // Mostrem el camp d'entrada per derrotar l'enemic


        }
    }
}

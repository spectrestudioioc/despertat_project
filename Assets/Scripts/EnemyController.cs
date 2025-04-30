using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager; // Referència a un component GameManager
    private Loot loot; // Referencia a un component Loot
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
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager
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

    public void ComprovarParaulaClau(string userInput)
    {
        if (userInput.Equals(paraulaClau, StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Paraula clau correcta!");
            ParaulaCorrecta(); // Aquí es crida el mètode si s'ha encertat la paraula
        }
        else
        {
            Debug.Log("Paraula clau incorrecta!");
            ParaulaIncorrecta();
        }
    }

    public void ParaulaCorrecta()
    {
        loot.AddToPlayerInventory();
        StartCoroutine(MortCombinada());
    }

    public void ParaulaIncorrecta()
    {
        // Aplicar dany al jugador per una paraula incorrecta
        if (playerHealth != null)
        {
            int danyIncorrecte = 25; // Dany que s'aplica per una paraula incorrecta
            playerHealth.TakeDamage(danyIncorrecte);
            Debug.Log($"El jugador ha rebut {danyIncorrecte} de dany per paraula incorrecta!");
        }
    }

    private IEnumerator MortCombinada()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        if (rend == null)
        {
            Debug.LogWarning("SkinnedMeshRenderer no trobat al fill de l'enemic.");
            yield break;
        }
        Color originalColor = rend.material.color;
        Vector3 escalaOriginal = transform.localScale;

        float tempsDesintegracio = 2f;  // Durada de la desintegració
        float tempsTranscorregut = 0f;

        while (tempsTranscorregut < tempsDesintegracio)
        {
            tempsTranscorregut += Time.deltaTime;

            // Reduir transparència
            float alpha = Mathf.Lerp(1f, 0f, tempsTranscorregut / tempsDesintegracio);
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Reduir la mida
            float factorEscala = Mathf.Lerp(1f, 0f, tempsTranscorregut / tempsDesintegracio);
            transform.localScale = escalaOriginal * factorEscala;

            yield return null;  // Espera al següent frame
        }

        // Desactiva l'enemic després de la mort
        gameObject.SetActive(false);


    }


}

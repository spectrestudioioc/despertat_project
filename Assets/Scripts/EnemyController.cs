using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager; // Referència a un component GameManager
    private Loot loot; // Referencia a un component Loot
    private Transform playerTransform; // Referència al Transform del jugador
    public bool areaEnemic;

    public AudioClip soMort; // Clip de so que volem reproduir quan mori
    private AudioSource audioSource;

    [Tooltip("ID del pickup necessari per interactuar amb aquest enemic.")]
    public int pickupID;


    // Definim els diferents tipus d’enemics
    public enum TipusEnemic
    {
        EnemicOmbra,
        EnemicRialla,
        EnemicGranPena
    }

    public TipusEnemic tipusEnemic; // Propietat per assignar el tipus d'Enemic
    public int dany;

    // Propietats que defineixen la paraula clau que mata l'enemic
    public string paraulaClau1; 
    public string paraulaClau2;

    private PlayerHealth playerHealth; // Referencia a un component PlayerHealth
    

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager
        loot = GetComponent<Loot>(); // Inicialitzem la referència al component Loot

        // Obtenim la referència al component PlayerHealth
        playerHealth = FindObjectOfType<PlayerHealth>();

        // Assignem el Transform del jugador a partir del component PlayerHealth, si existeix
        playerTransform = playerHealth?.transform;

        // Assignem el valor de dany segons el tipus d’enemic
        switch (tipusEnemic)
        {
            case TipusEnemic.EnemicOmbra:
                dany = 5;
                break;
            case TipusEnemic.EnemicRialla:
                dany = 10;
                break;
            case TipusEnemic.EnemicGranPena:
                dany = 15;
                break;
            default:
                break;
        }

        audioSource = GetComponent<AudioSource>();

    }

    // Mètode que rep una instància d'EnemyCollider i aplica dany al jugador
    public void TakeDamage(EnemyCollider enemyCollider)
    {
        
        if (playerHealth != null)
        {

            playerHealth.TakeDamage(dany); // Restem la vida al jugador
            // Mostrem un missatge a la consola cada vegada que apliquem dany
            Debug.Log($"S'ha aplicat {dany} de dany al jugador. Salut restant: {playerHealth.currentHealth}");
        }
    }

    // Comprova si la paraula introduïda pel jugador és correcta
    public void ComprovarParaulaClau(string userInput)
    {
        if (userInput.Equals(paraulaClau1, StringComparison.OrdinalIgnoreCase) || userInput.Equals(paraulaClau2, StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Paraula clau correcta!");
            ParaulaCorrecta(); // Aquí es crida el mètode si s'ha encertat la paraula
        }
        else
        {
            Debug.Log("Paraula clau incorrecta!");
            ParaulaIncorrecta(); // Aquí es crida el mètode si s'ha fallat la paraula
        }
    }

    // Mètode que s’executa si la paraula clau introduida pel jugador és correcta
    public void ParaulaCorrecta()
    {
        // Comprova si el component Loot existeix abans de fer alguna cosa amb ell
        if (loot != null)
        {
            loot.AddToPlayerInventory(); // Afegeix el Loot a l’inventari del jugador
        }
        StartCoroutine(MortCombinada()); // Inicia mètode de desactivació de l’enemic
    }

    // Mètode que s’executa si la paraula clau introduida pel jugador és incorrecta
    public void ParaulaIncorrecta()
    {
        // Aplicar dany al jugador per una paraula incorrecta
        if (playerHealth != null)
        {
            int danyIncorrecte = 25; // Dany que s'aplica per una paraula incorrecta
            playerHealth.TakeDamage(danyIncorrecte);
            Debug.Log($"El jugador ha rebut {danyIncorrecte} de dany per paraula incorrecta!");
            gameManager.MostraEnemicText(this); // Aquí li passes el mateix enemic
        }
    }

    // Coroutine que gestiona la mort visual de l’enemic abans de desaparèixer
    private IEnumerator MortCombinada()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        if (rend == null)
        {
            Debug.LogWarning("SkinnedMeshRenderer no trobat al fill de l'enemic.");
            yield break;
        }

        if (soMort != null && audioSource != null)
        {
            audioSource.PlayOneShot(soMort);
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

        // Si l'enemi és el del tipus GranPena, cridem al mètode Victory() del GameManager
        if (tipusEnemic == TipusEnemic.EnemicGranPena)
        {
            gameManager.Victory();
        }


    }
    private void Update()
    {
        // Si aquest enemic és del tipus GranPena, sempre mirarà cap al jugador
        if (tipusEnemic == TipusEnemic.EnemicGranPena && playerTransform != null)
        {
            Vector3 direccio = playerTransform.position - transform.position;
            direccio.y = 0f; // Evita inclinació vertical

            if (direccio != Vector3.zero)
            {
                // Calculem la rotació desitjada (amb un gir de 180 graus per mirar enrere, ssegons l'eix Z)
                Quaternion rotacioDesitjada = Quaternion.LookRotation(direccio) * Quaternion.Euler(0, 180f, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacioDesitjada, Time.deltaTime * 2f); // Gir suau
            }
        }
    }


}

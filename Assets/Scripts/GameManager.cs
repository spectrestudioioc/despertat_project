using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton per accedir fàcilment al GameManager
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI enemicText;
    public TMP_InputField enemicInputField;

    public Slider healthSlider;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostraPickupText(PickupController pickup)
    {
        pickupText.gameObject.SetActive(true); // Assegurem que el text estigui visible
        pickupText.text = "Prem la tecla E per recollir"; // Mostrem el missatge per recollir
    }

    public void AmagaPickupText(PickupController pickup)
    {
        pickupText.gameObject.SetActive(false); // Assegurem que el text ja no sigui visible       
    }

    public void RecullPickup(PickupController pickup)
    {
        AmagaPickupText(pickup);
        pickup.gameObject.SetActive(false);
    }

    public void MostraEnemicText(EnemyController enemic)
    {
        enemicText.gameObject.SetActive(true); // Assegurem que el text estigui visible
        enemicText.text = "Prem la tecla E per interactuar amb l'enemic"; // Mostrem el missatge per interactuar amb l'enemic
    }

    public void AmagaEnemicText(EnemyController enemic)
    {
        enemicText.gameObject.SetActive(false); // Assegurem que el text ja no sigui visible       
    }

    public void MostraEnemicInputField(EnemyController enemyController)
    {
        Debug.Log("S'ha cridat MostraEnemicInputField");
        enemicInputField.gameObject.SetActive(true);
        enemicInputField.Select(); // Això farà que el camp d'entrada es seleccionat i estigui preparat per escriure
        enemicInputField.ActivateInputField(); // Activa el camp d'entrada per escriure
        
        // Afegir un listener per capturar el text un cop es prem "Enter"
        enemicInputField.onEndEdit.AddListener((string userInput) => OnInputSubmitted(userInput, enemyController));
    }

    private void OnInputSubmitted(string userInput, EnemyController enemyController)
    {
        Debug.Log("Text introduït per l'usuari: " + userInput); // Mostrem el text a la consola

        // Passem la paraula recollida a EnemyController per fer la comparació
        enemyController.ComprovarParaulaClau(userInput);

        // Netegem i desactivem el camp d'entrada un cop l'usuari prem Enter
        enemicInputField.text = ""; 
        enemicInputField.gameObject.SetActive(false);
    }
}


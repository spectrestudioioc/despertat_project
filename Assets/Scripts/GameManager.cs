using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton per accedir f�cilment al GameManager
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI enemicText;
    public TMP_InputField enemicInputField;

    public Slider healthSlider;

    public Image diariImatge; // Imatge que per mostrar la p�gina del diari

    public int vidaJugador; // Vida persistent entre escenes

    
    private void Awake()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject canvasGUI = transform.Find("CanvasGUI")?.gameObject;
        if (canvasGUI != null)
        {
            // Amaga el CanvasGUI quan es carrega l'escena Video3Scene
            canvasGUI.SetActive(scene.name != "Video3Scene");
        }

        // Configura el cursor segons l'escena
        if (scene.name == "MainMenu" || scene.name == "Victory" || scene.name == "GameOver")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        // Reprodueix el so si s'ha assignat un clip
        if (pickup.pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickup.pickupSound, pickup.transform.position);
        }

        // Comprovem si �s un pickup de curaci� mitjan�ant el tag
        if (pickup.CompareTag("Medicaci�"))
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                int quantitatCura = 25;
                playerHealth.Heal(quantitatCura);
                Debug.Log($"Pickup de curaci� recollit. S'han curat {quantitatCura} punts de salut.");
            }
        }
        else
        {
            // Nom�s mostrem la p�gina si no �s un pickup de curaci�
            diariImatge.sprite = pickup.imatgePagina;
            MostraPaginaDiari();
        }
    }

    public void MostraPaginaDiari()
    {

        // Fem visible la p�gina del diari
        diariImatge.gameObject.SetActive(true);
        diariImatge.GetComponent<Animation>().Play("CanvasDiari");
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
        enemicInputField.Select(); // Aix� far� que el camp d'entrada es seleccionat i estigui preparat per escriure
        enemicInputField.ActivateInputField(); // Activa el camp d'entrada per escriure
        

        // Eliminem listeners anteriors
        enemicInputField.onEndEdit.RemoveAllListeners();
        // Afegir un listener per capturar el text un cop es prem "Enter"
        enemicInputField.onEndEdit.AddListener((string userInput) => OnInputSubmitted(userInput, enemyController));
    }

    private void OnInputSubmitted(string userInput, EnemyController enemyController)
    {
        Debug.Log("Text introdu�t per l'usuari: " + userInput); // Mostrem el text a la consola

        // Passem la paraula recollida a EnemyController per fer la comparaci�
        enemyController.ComprovarParaulaClau(userInput);

        
        

        // Netegem i desactivem el camp d'entrada un cop l'usuari prem Enter
        enemicInputField.text = "";
        enemicInputField.gameObject.SetActive(false);
    }

    public void Victory()
    {
        // Canviem a escena Victory
        SceneManager.LoadScene("Victory");
        vidaJugador = 100;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segons abans de carregar l'escena
        SceneManager.LoadScene("GameOver");
        vidaJugador = 100;
    }
}


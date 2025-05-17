using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

/*
 * Aquesta classe centralitza la gestió del joc: 
 * mostra missatges a pantalla, gestiona la vida persistent del jugador,
 * interaccions amb pickups i enemics, i controla els canvis d’escena
 * */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton per accedir fàcilment al GameManager
    public TextMeshProUGUI pickupText; // Text per indicar que es pot recollir un objecte
    public TextMeshProUGUI enemicText; // Text per indicar que es pot interactuar amb un enemic
    public TMP_InputField enemicInputField; // Camp de text on l'usuari pot escriure la paraula clau
    public TextMeshProUGUI pistaAltellText; // Text per indicar les pistes de l'Altell (Nivell 2)

    public TextMeshProUGUI lootAfegitText; // Text per indicar el Loot afegit

    public Slider healthSlider;  // Barra de salut del jugador

    public Image diariImatge; // Imatge que per mostrar la pàgina del diari

    public int vidaJugador; // Vida del jugador persistent entre escenes

    // Llista de pickups que el jugador ha recollit
    private List<int> pickupsRecollits = new List<int>();


    private void Awake()
    {
        // Assegura que només hi ha una instància del GameManager
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
        // Subscripció per escoltar quan es carrega una nova escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desubscripció per evitar errors en escenes futures
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Amaga el CanvasGUI si entrem a l'escena "Video3Scene"
        GameObject canvasGUI = transform.Find("CanvasGUI")?.gameObject;
        if (canvasGUI != null)
        {
            
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

        // Reinicia les icones del diari del Menu Pausa en començar Nivell 1
        if (scene.name == "Nivell 1")
        {
            ReiniciaPartida();
            Debug.Log("[GameManager] Nivell 1 carregat. Actualitzant icones del diari...");

            IconaDiariController[] icones = FindObjectsOfType<IconaDiariController>();
            foreach (var icona in icones)
            {
                icona.ActualitzaEstat();
            }
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
        pickup.gameObject.SetActive(false); // Desactiva l'objecte recollit

        // Afegim l'ID del pickup a la llista de pickups recollits
        pickupsRecollits.Add(pickup.pickupID);
        Debug.Log($"Pickup amb ID {pickup.pickupID} afegit a la llista pickupsRecollits.");

        // Reprodueix el so si s'ha assignat un clip
        if (pickup.pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickup.pickupSound, pickup.transform.position);
        }

        // Comprovem si és un pickup de curació mitjançant el tag
        if (pickup.CompareTag("Medicació"))
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                int quantitatCura = 25;
                playerHealth.Heal(quantitatCura); // Passem el valor de quantitatCura al mètode Heal de PlayerHealth
                Debug.Log($"Pickup de curació recollit. S'han curat {quantitatCura} punts de salut.");
            }
        }
        else
        {
            // Només mostrem la pàgina si no és un pickup de curació
            diariImatge.sprite = pickup.imatgePagina;
            MostraPaginaDiari();
        }
    }

    public void MostraPaginaDiari()
    {

        // Fem visible la pàgina del diari, juntament amb la seva animació
        diariImatge.gameObject.SetActive(true);
        diariImatge.GetComponent<Animation>().Play("CanvasDiari"); 
    }

    // Aquesta funció comprova si el pickup amb l'ID donat ha estat recollit
    public bool HaRecollitPickup(int pickupID)
    {
        // Verifica si l'ID del pickup ha estat afegit a la llista de pickups recollits
        bool haRecollit = pickupsRecollits.Contains(pickupID);
        Debug.Log($"[CHECK] Pickup ID {pickupID} recollit? {haRecollit} | Total pickups recollits: {string.Join(", ", pickupsRecollits)}");
        return haRecollit;
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

        AmagaEnemicText(enemyController); // Aquí s'amaga el "Prem E"


        // Eliminem listeners anteriors
        enemicInputField.onEndEdit.RemoveAllListeners();
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

    public void Victory()
    {
        // Canviem a escena Victory
        SceneManager.LoadScene("Victory");
        vidaJugador = 100;
    }

    public void GameOver()
    {
        // Inicia una espera abans de canviar a l’escena de Game Over
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segons abans de carregar l'escena
        SceneManager.LoadScene("GameOver");
        vidaJugador = 100;
    }

    public void MostraPistaAltell()
    {
        if (pistaAltellText != null)
        {
            pistaAltellText.gameObject.SetActive(true);

            Animation anim = pistaAltellText.GetComponent<Animation>();
            if (anim != null)
            {
                anim.Play();
            }
            else
            {
                Debug.LogWarning("El text pistaAltell no té component Animation.");
            }
        }
        else
        {
            Debug.LogWarning("pistaAltellText no està assignat al GameManager.");
        }
    }
    public void AmagaPistaAltell()
    {
        StartCoroutine(PistaAltellDelay());
    }

    private IEnumerator PistaAltellDelay()
    {
        yield return new WaitForSeconds(10f);
        pistaAltellText.gameObject.SetActive(false);
    }

    public void MostraMissatgeClau(string item)
    {
        if (lootAfegitText != null)
        {
            lootAfegitText.text = $"L'Enemic tenia la {item}, l'has aconseguit!";
            lootAfegitText.gameObject.SetActive(true);
            StartCoroutine(AmagaMissatgeClau());
        }
    }

    private IEnumerator AmagaMissatgeClau()
    {
        yield return new WaitForSeconds(4f); // El missatge es mostra durant 4 segons
        lootAfegitText.gameObject.SetActive(false);
    }

    public void ReiniciaPartida()
    {
        pickupsRecollits.Clear();
        vidaJugador = 100;
        Debug.Log("Dades de partida reiniciades.");
    }
}


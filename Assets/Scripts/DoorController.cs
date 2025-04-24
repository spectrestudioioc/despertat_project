using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool areaPorta;
    public GameObject worldDisplay;
    public GameObject requirementDisplay;
    private AnimationPlayer animationPlayer;
    private BoxCollider triggerCollider;
    public TMP_Text requerimentError;
    private string missatgeError;
    [SerializeField] private Animation notificationAnimation;

    [SerializeField] private List<RequirementSO> doorRequirements; // Requisits per obrir la porta

    private void Start()
    {
        areaPorta = false;
        triggerCollider = GetComponent<BoxCollider>(); // Obtenim el BoxCollider que actua com a trigger

        // Agafem el component AnimationPlayer del mateix objecte
        animationPlayer = GetComponent<AnimationPlayer>();
        if (animationPlayer == null)
        {
            Debug.LogWarning("No s'ha trobat cap component AnimationPlayer al GameObject de la porta.");
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha entrat al trigger de la porta!");
            worldDisplay.SetActive(true);
            areaPorta = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha sortit del trigger de la porta!");
            worldDisplay.SetActive(false);
            areaPorta = false;
        }
    }

    private void openDoor()
    {
        if (animationPlayer != null)
        {
            animationPlayer.Play(); // Reproduïm l’animació a través del component AnimationPlayer
            worldDisplay.SetActive(false);
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
            }
        }
    }

    // Funció per validar els requisits de la porta
    private bool ValidateRequirements()
    {
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if (playerInventory == null)
        {
            Debug.Log("El jugador no té inventari");
            return false;
        }

        foreach (RequirementSO requirement in doorRequirements)
        {
            // Comprovació més senzilla per evitar l'error de referència nula
            if (requirement != null && !requirement.Validate(playerInventory.gameObject)) // Validem si el jugador compleix els requisits) 
            {
                if (requirement is RequirementInventorySO inventoryRequirement)
                {
                    string itemName = inventoryRequirement.ItemName;
                    missatgeError = "Falta la clau: " + itemName;
                    requerimentError.text = missatgeError;
                    requirementDisplay.SetActive(true);
                    notificationAnimation.Play();
                    Debug.Log($"No tens la clau necessària: {inventoryRequirement.ItemName}");

                }
                return false;

            }
        }

        return true;
    }

    private void Update()
    {
        if (areaPorta && Input.GetKeyDown(KeyCode.E)) // Comprovem si el jugador està dins del trigger i prem la tecla E
        {
            Debug.Log("El jugador ha clicat la tecla E davant la porta");

            // Comprovem si els requisits es compleixen abans d'obrir la porta
            if (ValidateRequirements())
            {
                openDoor();
            }
        }
    }
}

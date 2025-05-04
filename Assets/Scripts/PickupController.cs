using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aquesta classe gestiona els pickups, incloent la detecció del Player i la seva recollida 
public class PickupController : MonoBehaviour
{
    private GameManager gameManager; // Referència a un component GameManager

    public Sprite imatgePagina;  // Propietat per assignar la imatge del pickup

    public AudioClip pickupSound; // Propietat per assignar Clip d'audio

    public bool areaPickup;

    private void Start()
    {
        gameManager = GameManager.Instance; // Obtenim la instància del GameManager
        areaPickup = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha entrat al trigger del pickup!");
            gameManager.MostraPickupText(this); 
            areaPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Comprova si el GameObject que toca és el Player
        {
            Debug.Log("El player ha sortit del trigger del pickup!");
            gameManager.AmagaPickupText(this);
            areaPickup = false;
        }
    }

    private void Update()
    {
        if (areaPickup && Input.GetKeyDown(KeyCode.E)) // Comprovem si el jugador està dins del trigger i prem la tecla E
        {
            Debug.Log("El jugador ha clicat la tecla E davant del pickup");

            // Li passa al GameManager la referencia concreta del pickup que estem gestionant per cridar el mètode RecullPickup
            gameManager.RecullPickup(this); 
        }
    }
}

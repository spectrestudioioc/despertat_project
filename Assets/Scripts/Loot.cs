using System;
using System.Collections.Generic;
using UnityEngine;


// Aquesta classe gestiona el Loot que incorporen els enemics, a trav�s del m�tode AddToPlayerInventory
public class Loot : MonoBehaviour
{
    [SerializeField] List<String> loot = new List<string>(); // Llista d'objectes que es poden recollir

    private Inventory _inventory; // Refer�ncia a l'inventari del jugador

    
    private void Start()
    {
        // Es busca si hi ha algun gameObject que tingui el component Inventory
        _inventory = FindObjectOfType<Inventory>();
    }

    // M�tode per afegir el Loot a l'Inventary que t� el jugador
    public void AddToPlayerInventory()
    {
        foreach (String item in loot)
        {
            _inventory.Add(item);
            Debug.Log($"Afegit {item} a l'inventari");
            
            GameManager.Instance.MostraMissatgeClau(item); // Mostrem missatge per pantalla a trav�s del GameManager
        }

        loot.Clear();
    }
}

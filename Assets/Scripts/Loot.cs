using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component simple que permet a altres elements cridar al m�tode AttToPlayerInventory (per exemple, un interactables)
 * per afegir tots els items configurats a l'inventari del jugador.
 */
public class Loot : MonoBehaviour
{
    [SerializeField] List<String> loot = new List<string>(); // Llista d'objectes que es poden recollir

    private Inventory _inventory; // Refer�ncia a l'inventari del jugador

    /**
     * Inicialitza la refer�ncia a l'inventari del jugador.
     * 
     * Com que nom�s el jugador t� inventari, es busca directament a l'escena.
     */
    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    /**
     * Afegeix tots els objectes configurats a l'inventari del jugador.
     * 
     * Despr�s de transferir els objectes, buida la llista de loot.
     */
    public void AddToPlayerInventory()
    {
        foreach (String item in loot)
        {
            _inventory.Add(item);
            Debug.Log($"Afegit {item} a l'inventari");
        }

        loot.Clear();
    }
}

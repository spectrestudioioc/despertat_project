using System;
using System.Collections.Generic;
using UnityEngine;


// Aquesta classe gestiona l'inventari que té el jugador
public class Inventory : MonoBehaviour
{
    public delegate void OnEventInventoryDelegate(String item); // Delegat per gestionar esdeveniments de l'inventari

    public event OnEventInventoryDelegate OnAddItem; // Esdeveniment que s'activa quan s'afegeix un element
    public event OnEventInventoryDelegate OnRemoveItem; // Esdeveniment que s'activa quan s'elimina un element

    [SerializeField] private List<String> items; // Llista d'objectes a l'inventari

    

    // Mètode per afegir el Loot recollit a l'inventari del jugador
    public void Add(String item)
    {
        if (OnAddItem != null) OnAddItem(item);
        
        items.Add(item);
    }

    
    // Mètode que activa l'esdeveniment corresponent en fer ús del Loot existent a l'Inventory. Després elimina l'item de l'inventari.
    public void Remove(String item)
    {
        if (OnRemoveItem != null) OnRemoveItem(item);
        
        items.Remove(item);
    }

    
    // Mètode per comprovar si existeix un ítem a l'Inventory
    public bool Has(String item)
    {
        return items.Contains(item);
    }

   

    // Mètode per fer ús de l'ítem que hi ha a l'Inventory. Posteriorment a usarlo, l'elimina
    public bool ConsumeItem(String item)
    {
        if (items.Contains(item))
        {
            Remove(item);
            return true;
        }
        else
        {
            return false;
        }
    }
}

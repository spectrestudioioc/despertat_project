using System;
using System.Collections.Generic;
using UnityEngine;


// Aquesta classe gestiona l'inventari que t� el jugador
public class Inventory : MonoBehaviour
{
    public delegate void OnEventInventoryDelegate(String item); // Delegat per gestionar esdeveniments de l'inventari

    public event OnEventInventoryDelegate OnAddItem; // Esdeveniment que s'activa quan s'afegeix un element
    public event OnEventInventoryDelegate OnRemoveItem; // Esdeveniment que s'activa quan s'elimina un element

    [SerializeField] private List<String> items; // Llista d'objectes a l'inventari

    

    // M�tode per afegir el Loot recollit a l'inventari del jugador
    public void Add(String item)
    {
        if (OnAddItem != null) OnAddItem(item);
        
        items.Add(item);
    }

    
    // M�tode que activa l'esdeveniment corresponent en fer �s del Loot existent a l'Inventory. Despr�s elimina l'item de l'inventari.
    public void Remove(String item)
    {
        if (OnRemoveItem != null) OnRemoveItem(item);
        
        items.Remove(item);
    }

    
    // M�tode per comprovar si existeix un �tem a l'Inventory
    public bool Has(String item)
    {
        return items.Contains(item);
    }

   

    // M�tode per fer �s de l'�tem que hi ha a l'Inventory. Posteriorment a usarlo, l'elimina
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

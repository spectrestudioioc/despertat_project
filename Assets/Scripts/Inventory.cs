using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component per afegir un sistema d'inventari simple basat en cadenes de text.
 * 
 * Permet afegir, eliminar i comprovar si un element es troba a l'inventari.
 */
public class Inventory : MonoBehaviour
{
    public delegate void OnEventInventoryDelegate(String item); // Delegat per gestionar esdeveniments de l'inventari

    public event OnEventInventoryDelegate OnAddItem; // Esdeveniment que s'activa quan s'afegeix un element
    public event OnEventInventoryDelegate OnRemoveItem; // Esdeveniment que s'activa quan s'elimina un element

    [SerializeField] private List<String> items; // Llista d'objectes a l'inventari

    /**
     * Afegeix un element a l'inventari i activa l'esdeveniment corresponent.
     * 
     * paràmetre: item- Nom de l'element a afegir.
     */
    public void Add(String item)
    {
        if (OnAddItem != null) OnAddItem(item);
        NotificationManager.Instance.ShowNotification($"Afegit <{item}> a l'inventari");
        items.Add(item);
    }

    /**
     * Elimina un element de l'inventari i activa l'esdeveniment corresponent.
     * 
     * paràmetre: item- Nom de l'element a eliminar.
     */
    public void Remove(String item)
    {
        if (OnRemoveItem != null) OnRemoveItem(item);
        NotificationManager.Instance.ShowNotification($"Eliminat <{item}> de l'inventari");
        items.Remove(item);
    }

    /**
     * Comprova si un element està a l'inventari.
     * 
     * paràmetre: item- Nom de l'element a comprovar.
     * @return True si l'element es troba a l'inventari, False en cas contrari.
     */
    public bool Has(String item)
    {
        return items.Contains(item);
    }

    /**
     * Consumeix un element de l'inventari (l'elimina si existeix).
     * 
     * paràmetre: item- Nom de l'element a consumir.
     * @return True si l'element ha estat consumit, False si no existia a l'inventari.
     */
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

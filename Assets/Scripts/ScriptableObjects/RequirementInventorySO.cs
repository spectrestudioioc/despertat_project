using System;
using UnityEngine;

/**
 * Requisit que verifica si el jugador té un objecte específic a l'inventari.
 * 
 * Aquest ScriptableObject comprova si el GameObject que intenta interactuar disposa
 * d'un inventari i si l'objecte especificat es troba dins d'aquest.
 */
[CreateAssetMenu(menuName = "Requirements/Inventory", fileName = "New Requirement Inventory")]
public class RequirementInventorySO : RequirementSO
{
    [SerializeField] public String ItemName; // Nom de l'objecte requerit

    /**
     * Valida si el GameObject té un inventari i si l'objecte especificat hi és present.
     * 
     * paràmetre: gameobject- GameObject que intenta interactuar.
     * @return True si té l'objecte requerit, False en cas contrari.
     */
    public override bool Validate(GameObject gameobject)
    {
        Inventory inventory = gameobject.GetComponentInParent<Inventory>();

        if (!inventory)
        {
            return false;
        }

        return inventory.Has(ItemName);
    }

    /**
     * Retorna un missatge d'error si no es compleix el requisit.
     */
    public override string GetErrorMessage()
    {
        return $"Es requereix <{ItemName}>!!";
    }
}


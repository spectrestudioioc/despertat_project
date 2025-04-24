using System;
using UnityEngine;

/**
 * Classe base per gestionar requisits d'interacció mitjançant ScriptableObjects.
 * 
 * Aquesta classe ha de ser estesa per implementar diferents tipus de requisits, com requisits d'inventari.
 */
abstract public class RequirementSO : ScriptableObject
{
    /**
     * Valida si el GameObject compleix el requisit.
     * 
     * paràmetre: gameobject- GameObject que intenta complir el requisit.
     * @return True si es compleix, False en cas contrari.
     */
    abstract public bool Validate(GameObject gameobject);

    /**
     * Retorna un missatge d'error si el requisit no es compleix.
     * 
     * Aquest mètode es pot sobreescriure per proporcionar missatges més específics.
     */
    virtual public String GetErrorMessage()
    {
        return "No s'acompleix el requisit";
    }
}


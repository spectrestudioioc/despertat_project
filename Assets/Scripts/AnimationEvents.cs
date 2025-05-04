using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Classe que serveix per amagar un gameObject quan es cridat. UAfegit com a component a CanvasDiari
 */
public class AnimationEvents : MonoBehaviour
{
    public void AmagaGameObject()
    {
        gameObject.SetActive(false); // Quan acabi l’animació, es desactiva
    }
}

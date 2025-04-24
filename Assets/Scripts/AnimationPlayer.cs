using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component auxiliar que permet reproduir animacions independents sense requerir un Animator.
 * Utilitza el component "Animation" en lloc de "Animator".
 */
public class AnimationPlayer : MonoBehaviour
{
    private Animation _animation; // Refer�ncia al component Animation

    /**
     * Inicialitza la refer�ncia al component Animation.
     */
    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    /**
     * Reprodueix l'animaci� actual del component Animation.
     */
    public void Play()
    {
        _animation.Play();
    }
}


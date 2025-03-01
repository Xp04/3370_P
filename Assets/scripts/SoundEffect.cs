using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class SoundEffect : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged "bomb_enemy"
        if (collision.gameObject.CompareTag("bomb_enemy"))
        {
            // Play the sound when the collision occurs
            audioSource.Play();
        }
    }
}

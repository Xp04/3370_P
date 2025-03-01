using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;
    public float respawnDelay = 2f;

    private GameObject currentPlayer;

    private void Awake()
    {
        instance = this;
    }

    public void RespawnPlayer()
    {
        // If a player already exists, destroy it
        if (currentPlayer != null)
        {
            Destroy(currentPlayer); // Destroy the old player
        }

        StartCoroutine(RespawnWithDelay());
        // Instantiate the new player at the respawn point
        //currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }


    private IEnumerator RespawnWithDelay()
    {
        // Wait for the specified time before respawning the player
        yield return new WaitForSeconds(respawnDelay);

        // Instantiate the new player at the respawn point
        currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
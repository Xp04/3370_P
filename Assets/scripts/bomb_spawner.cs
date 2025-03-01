using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_spawner : MonoBehaviour
{

    public GameObject itemPrefab;
    public float width = 10f;   // Horizontal width of spawn area
    public float height = 10f;  // Vertical height of spawn area
    public float respawnDelay = 3f;

    private List<GameObject> activeBombs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {  
        
        for(int i = 0; i < 10; i++)
        {
            spawnObject(); 
        }
       
    }

    // Update is called once per frame
    void Update()
    {

       // if(Input.GetKeyDown(KeyCode.B))
       // {
         //   spawnObject();
       // }
    }


    void spawnObject()
    {

        Vector3 randomPos = new Vector3(
            Random.Range(-width / 2, width / 2),  // X position within width
            Random.Range(-height / 2, height / 2), // Y position within height
            0);  // Z stays constant, adjust if needed for 3D
            GameObject bomb = Instantiate(itemPrefab, randomPos, Quaternion.identity);
            activeBombs.Add(bomb);
            bomb.AddComponent<Bomb>().OnCollected += HandleBombCollected;

    }

    void HandleBombCollected(GameObject bomb)
    {
        activeBombs.Remove(bomb);
        Destroy(bomb);
        StartCoroutine(RespawnBomb());
    }

    IEnumerator RespawnBomb()
    {
        yield return new WaitForSeconds(respawnDelay);
        spawnObject();
    }

}

public class Bomb : MonoBehaviour
{
    public delegate void CollectedEvent(GameObject bomb);
    public event CollectedEvent OnCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke(gameObject);
            score_manager.instance.deductPoints();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteries_producer : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject particles;
    public float width = 10f;   
    public float height = 10f;
    public float respawnDelay = 3f;

    private List<GameObject> activeBatteries = new List<GameObject>();


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
         //   spawObject();
       // }
    }



    void spawnObject()
    {

        
        Vector3 randomPos = new Vector3(
            Random.Range(-width / 2, width / 2),  // X position within width
            Random.Range(-height / 2, height / 2), // Y position within height
            0);  // Z stays constant, adjust if needed for 3D
        
        Instantiate(particles, randomPos, Quaternion.identity); // Spawn particles
        GameObject battery = Instantiate(itemPrefab, randomPos, Quaternion.identity);
        activeBatteries.Add(battery);

        battery.AddComponent<Battery>().OnCollected += HandleBatteryCollected;

    }

    void HandleBatteryCollected(GameObject battery)
    {
        activeBatteries.Remove(battery);
        Destroy(battery);
        StartCoroutine(RespawnBattery());
    }

    IEnumerator RespawnBattery()
    {
        yield return new WaitForSeconds(respawnDelay);
        spawnObject();
    }

    //void TriggerParticles(Vector3 position)
    //{
        //Instantiate(particles, position, Quaternion.identity); // Spawn particles at the item's position
    //}
}

public class Battery : MonoBehaviour
{
    public delegate void CollectedEvent(GameObject battery);
    public event CollectedEvent OnCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke(gameObject);
            score_manager.instance.addPoints();
        }
    }
}



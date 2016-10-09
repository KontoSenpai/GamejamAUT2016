using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject wanderer;
    public GameObject[] pickUps;  //0: turret, 1: wall, 2: mine

    public uint wandererSpawnLimit;
    public uint pickUpSpawnLimit;

    public uint wandererSpawnDelay;
    public uint pickUpSpawnDelay;

    private float timerWanderer;
    private float timerPickUp;
    private static uint numberOfWanderers;

    // Use this for initialization
    void Start () {

        timerWanderer = Time.time;
        timerPickUp = Time.time;
        SpawnPickUp();
	}
	
	// Update is called once per frame
	void Update () {

        if (CompareTag("WandererSpawner") && Time.time - timerWanderer >= wandererSpawnDelay && numberOfWanderers <= wandererSpawnLimit)
        {
            timerWanderer = Time.time;
            SpawnWanderer();
        }

        if (CompareTag("PickUpSpawner") && Time.time - timerPickUp >= pickUpSpawnDelay)
        {
            timerPickUp = Time.time;
            SpawnPickUp();
        }

    }

    public void SpawnPickUp()
    {
        int rand = Random.Range(0, 10);

        // 0 <= rand < 4    -> 40% turret
        // 4 <= rand < 8    -> 40% wall
        // 8 <= rand < 10   -> 20% mine
        if (rand < 4)       //turret
        {
            Instantiate(pickUps[0], transform.position, Quaternion.identity);
        }
        else if (rand > 7)  //mine
        {
            Instantiate(pickUps[2], transform.position, Quaternion.identity);
        }
        else                //wall
        {
            Instantiate(pickUps[1], transform.position, Quaternion.identity);
        }

    }

    public void SpawnWanderer()
    {
        Instantiate(wanderer, transform.position, Quaternion.identity);
        numberOfWanderers++;
    }
        
}

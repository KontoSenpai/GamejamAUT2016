using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject wanderer;

    public uint wandererSpawnLimit;
    public uint pickUpSpawnLimit;

    public uint wandererSpawnDelay;
    public uint pickUpSpawnDelay;

    private float timer;
    private static uint numberOfWanderers;

    // Use this for initialization
    void Start () {

        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - timer >= wandererSpawnDelay && numberOfWanderers <= wandererSpawnLimit)
        {
            timer = Time.time;
            SpawnWanderer();
        }

    }

    public void SpawnPickUp()
    {

    }

    public void SpawnWanderer()
    {
        Instantiate(wanderer, transform.position, Quaternion.identity);
        numberOfWanderers++;
    }
        
}

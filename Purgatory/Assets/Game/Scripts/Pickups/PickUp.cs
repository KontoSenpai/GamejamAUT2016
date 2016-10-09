using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    public GameObject pickUpObject;

    private float timeBeforeDestroyed;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        timeBeforeDestroyed = 10.0f;
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time - timer > timeBeforeDestroyed)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Player")
        {
            AddInventory(other.gameObject);
        }
    }

    public void AddInventory(GameObject player)
    {
        PlayerInventory inventory = player.gameObject.GetComponent<PlayerInventory>();
        if( inventory.GetItemInInventory() == null)
        {
            inventory.AddInventory(pickUpObject);
            Destroy(gameObject);
        }
    }
}

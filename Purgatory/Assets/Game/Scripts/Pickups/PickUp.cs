using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    public GameObject pickUpObject;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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
        print("SENT TO USER INVENTORY");
        PlayerInventory inventory = player.gameObject.GetComponent<PlayerInventory>();
        if( inventory.GetItemInInventory() == null)
        {
            inventory.AddInventory(pickUpObject);
            Destroy(gameObject);
        }
    }
}

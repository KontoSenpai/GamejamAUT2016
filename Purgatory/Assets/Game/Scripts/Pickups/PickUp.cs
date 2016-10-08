using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        AddInventory(gameObject);
        Destroy(gameObject);
    }

    public void AddInventory(GameObject type)
    {
        print("TO DO : Send to user inventory");
    }
}

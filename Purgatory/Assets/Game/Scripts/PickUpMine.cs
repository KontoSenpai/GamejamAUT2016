using UnityEngine;
using System.Collections;

public class PickUpMine : MonoBehaviour {

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
        if(other.GetType() == typeof(BoxCollider2D) && other.tag == "Player")
        {
            print("Mine Picked up");
            Destroy(gameObject);
        }
    }
}

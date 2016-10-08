using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderersInRadius : MonoBehaviour {

    private List<GameObject> wanderersInRadius;

    public List<GameObject> wanderers
    {
        get { return wanderersInRadius; }
    }
	// Use this for initialization
	void Start ()
    {
        wanderersInRadius = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Wanderers")
        {
            wanderersInRadius.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderers")
        {
            wanderersInRadius.Remove(other.gameObject);
        }
    }
}

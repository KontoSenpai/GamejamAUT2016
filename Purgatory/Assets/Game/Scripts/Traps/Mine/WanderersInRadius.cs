using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderersInRadius : MonoBehaviour {

    private List<GameObject> wanderersInRadius;

    public List<GameObject> GetWanderers()
    {
        return wanderersInRadius;
    }

	// Use this for initialization
	void Start ()
    {
        wanderersInRadius = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Wanderer" && wanderersInRadius.IndexOf(other.gameObject) == -1)
        {
            wanderersInRadius.Add(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer" && wanderersInRadius.IndexOf( other.gameObject) == -1)
        {
            wanderersInRadius.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer")
        {
            wanderersInRadius.Remove(other.gameObject);
        }
    }
}

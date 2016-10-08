using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineBehavior : MonoBehaviour {

    // 0 = Demon
    // 1 = Holy
    public Sprite[] mineType;

    WanderersInRadius explosionRadius;
    List<GameObject> wanderersInExplosionRadius;

	// Use this for initialization
	void Start ()
    {
        explosionRadius = gameObject.GetComponentInChildren<WanderersInRadius>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetOwner(bool bright)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        print(bright);
        if ( !bright)
            sprite.sprite = mineType[0];
        else
            sprite.sprite = mineType[1];
        sprite.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {/*
        //TODO, need to fuck replace the trigger condition with wanderer
        //TODO, nee to replace the destruction of wanderers to a conversion
        if( other.gameObject.tag == "Player")
        {
            wanderersInExplosionRadius = explosionRadius.wanderers;
            for(int i = 0; i < wanderersInExplosionRadius.Count; i++)
            {
                Destroy(wanderersInExplosionRadius[i]);
            }
            Destroy(gameObject);
        }*/
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineBehavior : MonoBehaviour {

    // 0 = Demon
    // 1 = Holy
    public Sprite[] mineType;
    private bool isOwnerDark = false;
    private bool isOwnerBright = false;

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
        {
            sprite.sprite = mineType[0];
            isOwnerDark = true;
        }
        else
        {
            sprite.sprite = mineType[1];
            isOwnerBright = true;
        }
        sprite.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Wanderer")
        {
            if( isOwnerBright = true && other.gameObject.GetComponent<Character>().getIsDark())
            {
                wanderersInExplosionRadius = explosionRadius.wanderers;
                for (int i = 0; i < wanderersInExplosionRadius.Count; i++)
                {
                    //TODO convert wanderers
                    //Destroy(wanderersInExplosionRadius[i]);
                }
                Destroy(gameObject);
            }
            else if( isOwnerDark = true && other.gameObject.GetComponent<Character>().getIsBright())
            {
                wanderersInExplosionRadius = explosionRadius.wanderers;
                for (int i = 0; i < wanderersInExplosionRadius.Count; i++)
                {
                    //TODO convert wanderers
                    //Destroy(wanderersInExplosionRadius[i]);
                }
                Destroy(gameObject);
            }
        }
    }

    void Explode()
    {
        print("toast");
    }
}

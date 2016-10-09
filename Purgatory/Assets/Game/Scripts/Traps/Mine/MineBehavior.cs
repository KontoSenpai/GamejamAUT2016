using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineBehavior : MonoBehaviour {

    // 0 = Demon
    // 1 = Holy
    public Sprite[] mineType;
    public GameObject[] remnantType;

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
        if( other.tag == "Wanderer")
        {
            Soul soul = other.GetComponent<Soul>();
            if( isOwnerBright == true && (soul.getIsDark() || (!soul.getIsBright() && !soul.getIsDark())))
            {
                wanderersInExplosionRadius = explosionRadius.GetWanderers();
                for (int i = 0; i < wanderersInExplosionRadius.Count; i++)
                {
                    Soul soulScript = wanderersInExplosionRadius[i].GetComponent<Soul>();
                    soulScript.Hit(isOwnerBright);
                }
                Instantiate(remnantType[1], transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if( isOwnerDark == true && ( soul.getIsBright() || ( !soul.getIsBright() && !soul.getIsDark() )))
            {
                wanderersInExplosionRadius = explosionRadius.GetWanderers();
                for (int i = 0; i < wanderersInExplosionRadius.Count; i++)
                {
                    Soul soulScript = wanderersInExplosionRadius[i].GetComponent<Soul>();
                    soulScript.Hit(isOwnerBright);
                }
                Instantiate(remnantType[0], transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void Explode()
    {
    }
}

using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    // 0 = Demon
    // 1 = Holy
    public Sprite[] towerType;
    public GameObject[] projectile;
    private GameObject shootingProjectile;
    private GameObject airProjectile;

    private bool isOwnerDark = false;
    private bool isOwnerBright = false;

    // TIME VARIABLES
    private float spawnTime;
    private float lastShotTime;

    //SHOOT VARIABLES
    private GameObject currentTarget;
    private Vector3 shootDirection;


    // Use this for initialization
    void Start ()
    {
        spawnTime = lastShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if( Time.time - spawnTime >= Constants.TOWER_LIFETIME)
        {
            Destroy(gameObject);
        }
        else if( currentTarget != null)
        {
            if(Time.time - lastShotTime >= Constants.TOWER_RATE_OF_FIRE)
            {
                ShootTarget();
            }
        }
	}

    /**Method used to define the owner of the placed trap
    * 
    *
    * @params bright : boolean used to distinguish players
    */
    public void SetOwner(bool bright)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (!bright)
        {
            sprite.sprite = towerType[0];
            shootingProjectile = projectile[0];
            isOwnerDark = true;
        }
        else
        {
            sprite.sprite = towerType[1];
            shootingProjectile = projectile[1];
            isOwnerBright = true;
        }
        sprite.enabled = true;
    }


    void ShootTarget()
    {
        GameObject projectile = (GameObject)Instantiate(shootingProjectile, transform.position, transform.rotation);
        projectile.transform.parent = gameObject.transform;
        if (isOwnerBright)
            projectile.GetComponent<ProjectileBehavior>().SetOwner(true, true);
        else if( isOwnerDark)
            projectile.GetComponent<ProjectileBehavior>().SetOwner(false, true);

        projectile.GetComponent<ProjectileBehavior>().SetTarget( currentTarget);
        lastShotTime = Time.time;
    }

    public void ResetAggro()
    {
        currentTarget = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer")
        {
            // Currently no target, so we get a new one
            if (currentTarget == null)
            {
                Soul soul = other.GetComponent<Soul>();
                if (isOwnerDark)
                    DarkOwner(soul, other.gameObject);
                else if (isOwnerBright)
                    BrightOwner(soul, other.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer")
        {
            // Currently no target, so we get a new one
            if (currentTarget == null)
            {
                Soul soul = other.GetComponent<Soul>();
                if (isOwnerDark)
                    DarkOwner(soul, other.gameObject);
                else if (isOwnerBright)
                    BrightOwner(soul, other.gameObject);
            }
        }
    }

    private void DarkOwner(Soul soul, GameObject other)
    {
        if (soul.getIsBright())
            currentTarget = other;
        else if (!soul.getIsBright() && !soul.getIsDark())
            currentTarget = other;
        else
            currentTarget = null;
    }

    private void BrightOwner(Soul soul, GameObject other)
    {
        if (soul.getIsDark())
            currentTarget = other.gameObject;
        else if (!soul.getIsBright() && !soul.getIsDark())
            currentTarget = other.gameObject;
        else
            currentTarget = null;
    }

}
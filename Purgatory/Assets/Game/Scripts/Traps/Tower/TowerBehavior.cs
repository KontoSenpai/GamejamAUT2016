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
            if(Time.time - lastShotTime >= Constants.RATE_OF_FIRE)
            {
                print("Tower Behavior - calling a shoot");
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
        print("Tower Behavior - Shooting a soul");
        GameObject projectile = (GameObject)Instantiate(shootingProjectile, transform.position, transform.rotation);
        if (isOwnerBright)
            projectile.GetComponent<TowerProjectileBehavior>().SetOwner(true, gameObject);
        else
            projectile.GetComponent<TowerProjectileBehavior>().SetOwner(false, gameObject);

        projectile.GetComponent<TowerProjectileBehavior>().SetTarget( currentTarget);
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
            print("Aiming a soul - Enter");
            // Currently no target, so we get a new one
            if (currentTarget == null)
            {
                Soul soul = other.GetComponent<Soul>();
                print("no target");
                if (isOwnerDark)
                {
                    print("Owner is dark");
                    if (soul.getIsBright())
                        currentTarget = other.gameObject;
                    else if (!soul.getIsBright() && !soul.getIsDark())
                        currentTarget = other.gameObject;
                }
                else if (isOwnerBright)
                {
                    print("Owner is bright");
                    if (soul.getIsDark())
                        currentTarget = other.gameObject;
                    else if (!soul.getIsBright() && !soul.getIsDark())
                        currentTarget = other.gameObject;
                }
                // If the target is neither dark or bright
                else if (!other.GetComponent<Soul>().getIsBright() && !other.GetComponent<Soul>().getIsDark())
                    currentTarget = other.gameObject;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer")
        {
            print("Aiming a soul - Stay");
            // Currently no target, so we get a new one
            if (currentTarget == null)
            {
                Soul soul = other.GetComponent<Soul>();
                print("no target");
                if (isOwnerDark)
                {
                    print("Owner is dark");
                    if (soul.getIsBright())
                        currentTarget = other.gameObject;
                    else if (!soul.getIsBright() && !soul.getIsDark())
                        currentTarget = other.gameObject;
                }
                else if (isOwnerBright)
                {
                    print("Owner is bright");
                    if (soul.getIsDark())
                        currentTarget = other.gameObject;
                    else if (!soul.getIsBright() && !soul.getIsDark())
                        currentTarget = other.gameObject;
                }
            }
        }
    }

}
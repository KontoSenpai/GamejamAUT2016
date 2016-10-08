using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    // 0 = Demon
    // 1 = Holy
    public Sprite[] towerType;
    public GameObject[] projectile;
    private GameObject shootingProjectile;

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
        else if( Time.time - lastShotTime >= Constants.RATE_OF_FIRE /*&& currentTarget != null*/)
        {
            Shoot();
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

    /** Method that gonna attack a soul
    *
    *
    */
    void Shoot()
    {
        float step = 4 * Time.deltaTime;
        GameObject projectile = (GameObject)Instantiate(shootingProjectile, transform.position, transform.rotation);
        projectile.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), step);
        lastShotTime = Time.time;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wanderer")
        {
            if (currentTarget == null)
            {
                currentTarget = other.gameObject;
            }
            else
            {
                if (currentTarget.GetComponent<Soul>().getIsDark() && isOwnerDark)
                {
                    currentTarget = other.gameObject;
                }
            }
        }
    }

}
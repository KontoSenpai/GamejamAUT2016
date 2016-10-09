using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {

    // false = dark
    // true = bright
    private bool ownerSide;
    // false = player
    // true = tower
    private bool ownerType;

    // Variables of player projectiles
    private Vector3 projectileDirection;

    // Variables of tower projectiles
    private GameObject target;

    private float creationTime;

    public GameObject GetTarget()
    {
        return target;
    }
    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    // Use this for initialization
    void Start()
    {
        creationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime >= Constants.PLAYER_PROJECTILE_LIFESPAN)
            Destroy(gameObject);

        // Un joueur
        if (ownerType == false)
        {
            float step = Constants.PLAYER_PROJECTILE_SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position+projectileDirection, step);
        }
        else // Une tour
        {
            if (target != null)
            {
                if (transform.position != target.transform.position)
                {
                    float step = Constants.TOWER_PROJECTILE_SPEED * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(ownerType == true)
        {
            if (other.gameObject == target)
            {
                target.GetComponent<Soul>().Hit(ownerSide);
                gameObject.transform.parent.GetComponent<TowerBehavior>().ResetAggro();
                Destroy(gameObject);
            }
        }
        else
        {
            if( other.tag == "Wanderer")
            {
                other.GetComponent<Soul>().Hit(ownerSide);
                Destroy(gameObject);
            }
        }

    }


    public void SetTargetPosition(Vector3 target)
    {
        projectileDirection = target;
    }

    /**Method used to define the owner of the placed trap
* 
*
* @params bright : boolean used to distinguish players
*/
    public void SetOwner(bool bright, bool tower)
    {
        ownerType = tower;
        ownerSide = bright;
    }
}

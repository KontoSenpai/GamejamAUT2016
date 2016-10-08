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
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerType == false)
        {
            float step = Constants.PLAYER_PROJECTILE_SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, projectileDirection * 50, step);
        }
        else
        {
            if (target != null)
            {
                if (transform.position != target.transform.position)
                {
                    float step = Constants.TOWER_PROJECTILE_SPEED * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                }
                else
                {
                    target.GetComponent<Soul>().Hit(ownerSide);
                    gameObject.transform.parent.GetComponent<TowerBehavior>().ResetAggro();
                    Destroy(gameObject);
                }
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
        if (!bright)
        {
            ownerSide = false;
        }
        else
        {
            ownerSide = true;
        }
    }
}

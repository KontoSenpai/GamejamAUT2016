using UnityEngine;
using System.Collections;

public class TowerProjectileBehavior : MonoBehaviour {

    // false = dark;
    // true = bright;
    private bool owner;
    private GameObject target;
    private GameObject towerOwn;

    public GameObject GetTarget()
    {
        return target;
    }
    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if( target != null)
        {
            if( transform.position != target.transform.position)
            {
                float step = Constants.TOWER_PROJECTILE_SPEED * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
            else
            {
                target.GetComponent<Soul>().Hit(owner);
                towerOwn.GetComponent<TowerBehavior>().ResetAggro();
                Destroy(gameObject);
            }
        }
    }

    /**Method used to define the owner of the placed trap
* 
*
* @params bright : boolean used to distinguish players
*/
    public void SetOwner(bool bright, GameObject tower)
    {
        towerOwn = tower;
        if (!bright)
        {
            owner = false;
        }
        else
        {
            owner = true;
        }
    }
}

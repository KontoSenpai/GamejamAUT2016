using UnityEngine;
using System.Collections;

public class TowerProjectileBehavior : MonoBehaviour {

    // false = dark;
    // true = bright;
    private bool owner;
    private GameObject target;

    public GameObject getTarget()
    {
        return target;
    }
    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    private Vector3 targetPos;

    public void SetTargetPos( Vector3 pos)
    {
        targetPos = pos;
    }
    public Vector3 GetTargetPos()
    {
        return targetPos;
    }
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if( target == null)
        {
            if( transform.position != targetPos)
            {
                print("Tower Projectile Behavior - Projectile not yet arrived");
                float step = 4 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            else
            {
                print("Tower Projectile Behavior - Projectile arrived at target position");
                //TODO convert the target
                Destroy(gameObject);
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

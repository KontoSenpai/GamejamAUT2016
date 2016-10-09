using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour
{

    // 0 : Evil
    // 1 : Holy
    public RuntimeAnimatorController[] animatorSprites;

    private float lifeStart;

    private bool isOwnerDark = false;
    private bool isOwnerBright = false;

    // Use this for initialization
    void Start()
    {

        lifeStart = Time.time;

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lifeStart >= Constants.TOWER_LIFETIME)
        {

            if( isOwnerDark)
                GameObject.FindObjectOfType<MapGrid>().substractValueToCell(transform.position, Constants.OBSTACLE_DARK);
            else if( isOwnerBright)
                GameObject.FindObjectOfType<MapGrid>().substractValueToCell(transform.position, Constants.OBSTACLE_BRIGHT);

            Destroy(gameObject);

        }

    }

    public void SetOwner(bool bright)
    {
        Animator animator = GetComponent<Animator>();
        print("Placed Wall");
        if (!bright)
        {
            animator.runtimeAnimatorController = animatorSprites[0];
            isOwnerDark = true;
            GameObject.FindObjectOfType<MapGrid>().addValueToCell(transform.position, Constants.OBSTACLE_DARK);

        }
        else
        {
            animator.runtimeAnimatorController = animatorSprites[1];
            isOwnerBright = true;
            GameObject.FindObjectOfType<MapGrid>().addValueToCell(transform.position, Constants.OBSTACLE_BRIGHT);
        }
    }

}

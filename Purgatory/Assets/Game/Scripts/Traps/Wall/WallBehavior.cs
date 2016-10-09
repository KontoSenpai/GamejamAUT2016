using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {

    // 0 : Evil
    // 1 : Holy
    public RuntimeAnimatorController[] animatorSprites;

    private bool isOwnerDark = false;
    private bool isOwnerBright = false;

    // Use this for initialization
    void Start ()
    {
<<<<<<< HEAD

        lifeStart = Time.deltaTime;

=======
	
>>>>>>> af56226abe9e68aae530ef09ffb3470bef5f5da9
	}
	
	// Update is called once per frame
	void Update ()
    {
<<<<<<< HEAD

        if (Time.deltaTime - lifeStart >= Constants.TOWER_LIFETIME)
        {

            if(isOwnerDark)
                GameObject.FindObjectOfType<MapGrid>().substractValueToCell(transform.position, Constants.OBSTACLE_DARK);
            else
                GameObject.FindObjectOfType<MapGrid>().substractValueToCell(transform.position, Constants.OBSTACLE_BRIGHT);

            Destroy(gameObject);

        }

=======
	
>>>>>>> af56226abe9e68aae530ef09ffb3470bef5f5da9
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

using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {

    // 0 : Evil
    // 1 : Holy
    public RuntimeAnimatorController[] animatorSprites;

    private float lifeStart;

    private bool isOwnerDark = false;
    private bool isOwnerBright = false;

    // Use this for initialization
    void Start ()
    {

        lifeStart = Time.deltaTime;

        GameObject.FindObjectOfType<MapGrid>();

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Time.deltaTime - lifeStart >= Constants.TOWER_LIFETIME)
            Destroy(gameObject);

	}

    public void SetOwner(bool bright)
    {
        Animator animator = GetComponent<Animator>();
        print("Placed Wall");
        if (!bright)
        {
            animator.runtimeAnimatorController = animatorSprites[0];
            isOwnerDark = true;
        }
        else
        {
            animator.runtimeAnimatorController = animatorSprites[1];
            isOwnerBright = true;
        }
    }

}

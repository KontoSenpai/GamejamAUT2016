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
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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

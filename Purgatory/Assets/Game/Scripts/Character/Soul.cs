using UnityEngine;
using System.Collections;

public class Soul : Character {

    // 0 : Neutral
    // 1 : Dark
    // 2 : Bright
    public Sprite[] apparence;

    void Start()
    { }

    // Update is called once per frame
    /*void FixedUpdate ()
    {
        if (!getIsDark && !getIsBright)
            Wander();
	}

    public void changeColor()
    {
        setIsAngel(!getIsBright);
        setIsDemon(!getIsDark);
    }*/

    public void Wander()
    {
        
    }

    public void Hit(bool projectileOwner)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if ( projectileOwner)
        {
            setIsDark(false);
            setIsBright(true);
            sprite.sprite = apparence[2];
        }
        else
        {
            setIsDark(true);
            setIsBright(false);
            sprite.sprite = apparence[1];
        }
    }

}

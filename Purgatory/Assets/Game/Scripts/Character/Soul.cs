using UnityEngine;
using System.Collections;

public class Soul : Character {

    // 0 : Neutral
    // 1 : Dark
    // 2 : Bright
    public Sprite[] apparence;
    public float wanderDelay;

    private float timer;
    private Wander wander;
    private bool isWandering = false;

    new void Start()
    {
        base.Start();
        wander = GetComponent<Wander>();
        timer = Time.time;

    }

    // Update is called once per frame
    new void Update ()
    {
        if(!isWandering)
        {
            isWandering = true;
            wander.Wandering();
        }

        base.Update();

        /*if (!getIsDark() && !getIsBright())
            if (Time.time - timer >= 2.0f)
            {
                stopMovement();
                Wander();
                timer = Time.time;
            }*/
	}

    public void Wander()
    {
        uint rand = (uint)Random.Range(0, 4);

        switch(rand)
        {
            case Constants.UP:
                moveUp();
                break;
            case Constants.DOWN:
                moveDown();
                break;
            case Constants.LEFT:
                moveLeft();
                break;
            case Constants.RIGHT:
                moveRight();
                break;
        }
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

using UnityEngine;
using System.Collections;

public class Soul : Character {

    // 0 : Neutral
    // 1 : Dark
    // 2 : Bright
    public Sprite[] apparence;

    private float timer;
    private Wander wander;
    private bool isWandering = false;

    new void Start()
    {
        base.Start();
        wander = GetComponent<Wander>();
        timer = Time.time;

        setIsBright(false);
        setIsDark(false);
    }

    // Update is called once per frame
    new void Update ()
    {

        if (!getIsDark() && !getIsBright())
            if (Time.time - timer >= Constants.WANDER_DELAY)
            {
                wander.Wandering();
                timer = Time.time;
            }


        base.Update();

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

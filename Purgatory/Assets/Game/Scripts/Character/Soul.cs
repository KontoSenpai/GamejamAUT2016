﻿using UnityEngine;
using System.Collections;

public class Soul : Character {

    // 0 : Neutral
    // 1 : Dark
    // 2 : Bright
    public RuntimeAnimatorController[] apparence;

    private float timer;
    private Wander wander;

    private MapGrid m_map;

    new void Start()
    {
        base.Start();
        timer = Time.time;
        m_map = GameObject.FindObjectOfType<MapGrid>();
        wander = GetComponent<Wander>();
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = apparence[0];

        Color cool = GetComponent<SpriteRenderer>().material.color;
        cool.a = 0.7f;
        GetComponent<SpriteRenderer>().material.color = cool;

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
        if ( projectileOwner && !getIsBright())
        {
            GetComponent<AudioSource>().Play();
            setIsDark(false);
            setIsBright(true);
            wander.StopWandering();
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = apparence[2];
            GetComponent<Seeker>().seek(m_map.getAngelBaseCoord());

            Color cool = GetComponent<SpriteRenderer>().material.color;
            cool.a = 0.7f;
            GetComponent<SpriteRenderer>().material.color = cool;
        }
        else if (!projectileOwner && !getIsDark())
        {
            GetComponent<AudioSource>().Play();
            setIsDark(true);
            setIsBright(false);
            wander.StopWandering();
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = apparence[1];
            GetComponent<Seeker>().seek(m_map.getDemonBaseCoord());
 
            Color cool = GetComponent<SpriteRenderer>().material.color;
            cool.a = 0.7f;
            GetComponent<SpriteRenderer>().material.color = cool;
        }
        m_speed += 0.5f;
    }

}

using UnityEngine;
using System.Collections;

public class Player : Character {

	// Use this for initialization
	void Start () {

        base.Start();


	}

    void Update()
    {

        if (Input.GetAxis("Horizontal") > 0.5)
            moveRight();
        else if (Input.GetAxis("Horizontal") < -0.5)
            moveLeft();
        else if (Input.GetAxis("Horizontal") < 0.5 &&
                Input.GetAxis("Horizontal") > -0.5)
            stopHorizontalMovement();

        if (Input.GetAxis("Vertical") > 0.5)
            moveUp();
        else if (Input.GetAxis("Vertical") < -0.5)
            moveDown();
        else if (Input.GetAxis("Vertical") < 0.5 &&
                 Input.GetAxis("Vertical") > -0.5)
            stopVerticalMovement();

    }	

}

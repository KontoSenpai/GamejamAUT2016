using UnityEngine;
using System.Collections;

public class Player : Character {

	public GameObject[] shoot;
	private GameObject shootingProjectile;
	private Transform shootSpawn;
	private Vector3 velocity = Vector3.zero;
	private float nextFire;

    private char m_playerIdentifier;

    // Use this for initialization
    new void Start ()
    {
        base.Start();
        if (gameObject.name.Contains("Angel"))
        {
            shootingProjectile = shoot[1];
            setIsBright(true);
            m_playerIdentifier = Constants.ANGEL;
        }
        else if (gameObject.name.Contains("Demon"))
        {
            shootingProjectile = shoot[0];
            setIsDark(true);
            m_playerIdentifier = Constants.DEMON;
        }
    }

    void Update()
    {

        //Debug.Log("Horizontal" + m_playerIdentifier);

        //Input are arrow keys and the left joystick
        if (Input.GetAxis("Horizontal" + m_playerIdentifier) > 0.5)
            moveRight();
        else if (Input.GetAxis("Horizontal" + m_playerIdentifier) < -0.5)
            moveLeft();
        else if (Input.GetAxis("Horizontal" + m_playerIdentifier) < 0.5 &&
                Input.GetAxis("Horizontal" + m_playerIdentifier) > -0.5)
            stopHorizontalMovement();

        if (Input.GetAxis("Vertical" + m_playerIdentifier) > 0.5)
            moveUp();
        else if (Input.GetAxis("Vertical" + m_playerIdentifier) < -0.5)
            moveDown();
        else if (Input.GetAxis("Vertical" + m_playerIdentifier) < 0.5 &&
                 Input.GetAxis("Vertical" + m_playerIdentifier) > -0.5)
            stopVerticalMovement();

        //To limit the fire rate
        if (Time.time > nextFire) {
            nextFire = Time.time + Constants.PLAYER_RATE_OF_FIRE;

            //Shooting with the right joystick
            if (Input.GetAxis("HorizontalAiming" + m_playerIdentifier) != 0 || Input.GetAxis("VerticalAiming" + m_playerIdentifier) != 0)
            {
                Vector3 jShootingDirection = new Vector3(Input.GetAxis("HorizontalAiming" + m_playerIdentifier), -(Input.GetAxis("VerticalAiming" + m_playerIdentifier)), 0.0f);
                aimNShoot(jShootingDirection);
            }

            //if (Input.GetAxis("HorizontalAimingKey") != 0 || Input.GetAxis("VerticalAimingKey") != 0)
            //{
            //    Vector3 kShootingDirection = new Vector3(Input.GetAxis("HorizontalAimingKey"), Input.GetAxis("VerticalAimingKey"), 0.0f);
            //    aimNShoot(kShootingDirection);
            //}

            //Shooting with the mouse
            if (Input.GetMouseButton(0))
            {
                Vector3 mShootingDirection = Input.mousePosition;
			    mShootingDirection.z = 10.0f;
			    Debug.Log(Camera.main.ScreenToWorldPoint(mShootingDirection));
			    mShootingDirection = Camera.main.ScreenToWorldPoint (mShootingDirection);
			    mShootingDirection = mShootingDirection - transform.position;
			    aimNShoot(mShootingDirection);
              }
            //DONT DELETE IT WORKS 
        }


    }	

	//public void aimNShoot(float rightStickX, float rightStickY) {
	public void aimNShoot(Vector3 shootingDir) {

        /*shootSpawn.position = gameObject.GetComponent<Transform>().position; 

		shootSpawn.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(mouse.x,  mouse.y) 
			* Mathf.Rad2Deg, transform.eulerAngles.z);
		
		shootSpawn.rotation = Quaternion.Euler (shootSpawn.eulerAngles);
        */

        GameObject projectile = (GameObject)Instantiate (shootingProjectile, transform.position, transform.rotation);

		projectile.GetComponent<ShootProjectile> ().SetTargetPosition(shootingDir);

	}

}

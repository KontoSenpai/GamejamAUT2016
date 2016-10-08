using UnityEngine;
using System.Collections;

public class Player : Character {

	public GameObject[] shoot;
	private GameObject shootingProjectile;
	private Transform shootSpawn;
	private Vector3 velocity = Vector3.zero;

	public float fireRate;
	private float nextFire;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
		if (gameObject.name.Contains ("Demon")) {
			shootingProjectile = shoot [0];
			setIsBright (true);
		} else {
			shootingProjectile = shoot [1];
			setIsDark (true);
		}
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

		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			/*
			var v3 = Input.mousePosition;
			v3.z = 10.0f;
			Debug.Log(Camera.main.ScreenToWorldPoint(v3));
			*/

			Vector3 shootDirection = Input.mousePosition;
			shootDirection.z = 10.0f;
			Debug.Log(Camera.main.ScreenToWorldPoint(shootDirection));
			shootDirection = Camera.main.ScreenToWorldPoint (shootDirection);
			shootDirection = shootDirection - transform.position;
			aimNShoot(shootDirection);
		}//if (Input.GetAxis ("HorizontalAiming") != 0 || Input.GetAxis ("VerticalAiming") != 0) {
		//	aimNShoot(Input.GetAxis ("HorizontalAiming"), Input.GetAxis ("VerticalAiming"));
		//}


    }	

	//public void aimNShoot(float rightStickX, float rightStickY) {
	public void aimNShoot(Vector3 mouse) {
		
		/*shootSpawn.position = gameObject.GetComponent<Transform>().position; 

		shootSpawn.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(mouse.x,  mouse.y) 
			* Mathf.Rad2Deg, transform.eulerAngles.z);
		
		shootSpawn.rotation = Quaternion.Euler (shootSpawn.eulerAngles);
*/
		GameObject projectile = (GameObject)Instantiate (shootingProjectile, transform.position, transform.rotation);

		//velocity = new Vector3 (mouse.x-transform.position.x
		//	, mouse.y-transform.position.y, 0.0f);
//		projectile.GetComponent<ShootProjectile> ().SetVelocity(velocity);
		//mouse.z = transform.position.z;
		projectile.GetComponent<ShootProjectile> ().SetMousePosition(mouse);

	}

}

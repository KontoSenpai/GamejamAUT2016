using UnityEngine;
using System.Collections;
using InControl;

public class Player : Character {

	public GameObject[] shoot;
	private GameObject projectile;
	private Transform shootSpawn;

    private bool isPressed = false;

	private Vector3 velocity = Vector3.zero;

	private float nextFire;
    private float fireCD;

    private uint m_playerIdentifier;

    private InputDevice player;

    private int xboxCount;

    private uint[] xboxControllerIndices;

    // Use this for initialization
    new void Start ()
    {
        base.Start();
        if (gameObject.name.Contains("Angel"))
        {
            projectile = shoot[1];
            setIsBright(true);
            m_playerIdentifier = Constants.ANGEL;
        }
        else if (gameObject.name.Contains("Demon"))
        {
            projectile = shoot[0];
            setIsDark(true);
            m_playerIdentifier = Constants.DEMON;
        }

        xboxControllerIndices = new uint[10];
    }

    new void Update()
    {

        //--------------------------------
        xboxCount = 0;

        for (int i = 0, k = 0; i<InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i].Name.Contains("XBox"))
            {
                xboxControllerIndices[k++] = (uint)i;
                xboxCount++;
            }
        }

        if (xboxCount == 2)
        {
            player = InputManager.Devices[(int)xboxControllerIndices[(int)m_playerIdentifier - 1]];

            PlayerMovement(player);

            if (player.Action1 == true && isPressed == false)
            {
                GetComponent<PlayerInventory>().UseItem();
                isPressed = true;
            }
            else if( player.Action1 == false && isPressed == true)
                isPressed = false;

            //To limit the fire rate
            if (Time.time - fireCD >= Constants.PLAYER_RATE_OF_FIRE)
            {
                PlayerFireHandle(player);
            }
        }
        //----------------------

        if (xboxCount == 1)
        {
            if (m_playerIdentifier == 1)
            {
                player = InputManager.Devices[(int)xboxControllerIndices[0]];

                PlayerMovement(player);

                if (player.Action1 == true && isPressed == false)
                {
                    GetComponent<PlayerInventory>().UseItem();
                    isPressed = true;
                }
                else if (player.Action1 == false && isPressed == true)
                    isPressed = false;
            }
            else
            {
                //Input are arrow keys for player2

                if (Input.GetAxis("HorizontalKey") > 0.5)
                    moveRight();
                else if (Input.GetAxis("HorizontalKey") < -0.5)
                    moveLeft();
                else if (Input.GetAxis("HorizontalKey") < 0.5 &&
                        Input.GetAxis("HorizontalKey") > -0.5)
                    stopHorizontalMovement();

                if (Input.GetAxis("VerticalKey") > 0.5)
                    moveUp();
                else if (Input.GetAxis("VerticalKey") < -0.5)
                    moveDown();
                else if (Input.GetAxis("VerticalKey") < 0.5 &&
                         Input.GetAxis("VerticalKey") > -0.5)
                    stopVerticalMovement();
                    
            }

            //To limit the fire rate
            if (Time.time > nextFire)
            {
                nextFire = Time.time + Constants.PLAYER_RATE_OF_FIRE;

                //Shooting with the right joystick
                if (m_playerIdentifier == 1 && (player.RightStickX != 0 || player.RightStickY != 0))
                {
                    Vector3 jShootingDirection = new Vector3(player.RightStickX, player.RightStickY, 0.0f);
                    aimNShoot(jShootingDirection);
                }
                else if (m_playerIdentifier == 2)
                {
                    if (Input.GetAxis("HorizontalAimingKey") != 0 || Input.GetAxis("VerticalAimingKey") != 0)
                    {
                        Vector3 kShootingDirection = new Vector3(Input.GetAxis("HorizontalAimingKey"), Input.GetAxis("VerticalAimingKey"), 0.0f);
                        aimNShoot(kShootingDirection);
                    }

                    //Shooting with the mouse
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 mShootingDirection = Input.mousePosition;
                        mShootingDirection.z = 10.0f;
                        Debug.Log(Camera.main.ScreenToWorldPoint(mShootingDirection));
                        mShootingDirection = Camera.main.ScreenToWorldPoint(mShootingDirection);
                        mShootingDirection = mShootingDirection - transform.position;
                        aimNShoot(mShootingDirection);
                    }

                }
            }
        }

        base.Update();
    }
    

    public void PlayerMovement(InputDevice player)
    {
        if (player.LeftStickX > 0.5)
            moveRight();
        else if (player.LeftStickX < -0.5)
            moveLeft();
        else if (player.LeftStickX < 0.5 &&
                player.LeftStickX > -0.5)
            stopHorizontalMovement();

        if (player.LeftStickY > 0.5)
            moveUp();
        else if (player.LeftStickY < -0.5)
            moveDown();
        else if (player.LeftStickY < 0.5 &&
                 player.LeftStickY > -0.5)
            stopVerticalMovement();
    }

    public void PlayerFireHandle( InputDevice player)
    {
        Vector3 pute = new Vector3();
        pute = Vector3.zero;

        //Shooting with the right joystick
        if (player.RightStick.X >= 0.3)
            pute.x = 1;
        else if (player.RightStick.X <= -0.3)
            pute.x = -1;
        if (player.RightStick.Y >= 0.3)
            pute.y = 1;
        else if (player.RightStick.Y <= -0.3)
            pute.y = -1;
        if (pute.x != 0 || pute.y != 0)
        {
            print("J'aim"+pute);
            aimNShoot(pute);
            fireCD = Time.time;
        }

        //Vector3 jShootingDirection = new Vector3(player.RightStickX, player.RightStickY, 0.0f); 

    }

    //public void aimNShoot(float rightStickX, float rightStickY) {
    public void aimNShoot(Vector3 shootingDir) {

        /*shootSpawn.position = gameObject.GetComponent<Transform>().position; 

		shootSpawn.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(mouse.x,  mouse.y) 
			* Mathf.Rad2Deg, transform.eulerAngles.z);
		
		shootSpawn.rotation = Quaternion.Euler (shootSpawn.eulerAngles);
        */

        GameObject instantiatedProjectile = (GameObject)Instantiate (projectile, transform.position, transform.rotation);

        if(getIsBright())
            instantiatedProjectile.GetComponent<ProjectileBehavior>().SetOwner( true, false);
        else if(getIsDark())
            instantiatedProjectile.GetComponent<ProjectileBehavior>().SetOwner(false, false);

        instantiatedProjectile.GetComponent<ProjectileBehavior> ().SetTargetPosition(shootingDir);

	}

}

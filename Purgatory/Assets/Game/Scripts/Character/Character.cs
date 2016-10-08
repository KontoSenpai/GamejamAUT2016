using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Vector2 m_position;
    public Vector2 basePosition;
    private Vector2 m_velocity = Vector2.zero;
    public float m_speed;

    private uint m_spriteDirection;

    private bool m_isDemon;
    private bool m_isAngel;

    private Animator m_animator;


    protected void Start () {

        m_position = basePosition;

        m_spriteDirection = Constants.DOWN;

        m_animator = GetComponent<Animator>();

	}
	
	// Moves Character
	protected void FixedUpdate ()
    {

        if (m_speed != 0.0f)
            m_position += m_velocity * m_speed * Time.deltaTime;

        if (m_animator)
        {

            m_animator.SetFloat("hSpeed", m_velocity.x);
            m_animator.SetFloat("vSpeed", m_velocity.y);

            m_animator.SetBool("isMoving", m_velocity.x > 0.1 || m_velocity.x < -0.1 ||
                                              m_velocity.y > 0.1 || m_velocity.y < -0.1);

        }

        transform.position = m_position;

	}

    // --- ACCESSORS ---

        // ** Getters **
    public Vector2 getPosition()        { return m_position; }
    public Vector2 getVelocity()        { return m_velocity; }
    public uint getSpriteDirection()    { return m_spriteDirection; }
    public bool getIsDemon()            { return m_isDemon; }
    public bool getIsAngel()            { return m_isAngel; }

        // ** Setters **
    public void setPosition(Vector2 position)               { m_position = position; }
    public void setSpriteDirection(uint spriteDirection)    { m_spriteDirection = spriteDirection; }
    public void setIsDemon(bool isDemon)                    { m_isDemon = isDemon; }
    public void setIsAngel(bool isAngel)                    { m_isAngel = isAngel; }


    // --- PUBLIC FUNCTIONS ---

    public void moveUp()    { m_velocity.y = 1.0f;  m_spriteDirection = Constants.UP; }
    public void moveDown()  { m_velocity.y = -1.0f; m_spriteDirection = Constants.DOWN; }
    public void moveLeft()  { m_velocity.x = -1.0f; m_spriteDirection = Constants.LEFT; }
    public void moveRight() { m_velocity.x = 1.0f;  m_spriteDirection = Constants.RIGHT; }

    public void stopVerticalMovement()   { m_velocity.y = 0.0f; }
    public void stopHorizontalMovement() { m_velocity.x = 0.0f; }

}

using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Vector2 m_position;
    public Vector2 basePosition;
    private Vector2 m_velocity = Vector2.zero;
    public float m_speed;

    private uint m_spriteDirection;

    private bool m_isDark;
    private bool m_isBright;

    private Animator m_animator;


    protected void Start () {

        m_spriteDirection = Constants.DOWN;

        m_animator = GetComponent<Animator>();

        m_position = transform.position;

	}
	
	// Moves Character
	protected void Update ()
    {

        if (m_velocity.magnitude != 1.0f)
            m_velocity.Normalize();

        if (m_speed != 0.0f)
            m_position += m_velocity * m_speed * Time.deltaTime;

        if (m_animator)
        {

            m_animator.SetFloat("hSpeed", m_velocity.x);
            m_animator.SetFloat("vSpeed", m_velocity.y);

            m_animator.SetBool("isMoving", m_velocity.x > 0.1 || m_velocity.x < -0.1 ||
                                           m_velocity.y > 0.1 || m_velocity.y < -0.1);

            m_animator.SetInteger("spriteDirection", (int)m_spriteDirection);

        }
        AllowMovement();
    }

    // --- ACCESSORS ---

        // ** Getters **
    public Vector2 getPosition()        { return m_position; }
    public Vector2 getVelocity()        { return m_velocity; }
    public uint getSpriteDirection()    { return m_spriteDirection; }
    public bool getIsDark()             { return m_isDark; }
    public bool getIsBright()           { return m_isBright; }

    public bool getIsMoving()           { return m_velocity.magnitude != 0; } 

        // ** Setters **
    public void setPosition(Vector2 position)               { m_position = position; }
    public void setSpriteDirection(uint spriteDirection)    { m_spriteDirection = spriteDirection; }
    public void setIsDark(bool isDark)                      { m_isDark = isDark; }
    public void setIsBright(bool isBright)                  { m_isBright = isBright; }


    // --- PUBLIC FUNCTIONS ---

    public void moveTo(Vector2 destination) { m_velocity = destination - m_position; }

    public void moveUp()    { m_velocity.y = 1.0f;  m_spriteDirection = Constants.UP; }
    public void moveDown()  { m_velocity.y = -1.0f; m_spriteDirection = Constants.DOWN; }
    public void moveLeft()  { m_velocity.x = -1.0f; m_spriteDirection = Constants.LEFT; }
    public void moveRight() { m_velocity.x = 1.0f;  m_spriteDirection = Constants.RIGHT; }

    public void stopMovement()           { stopVerticalMovement(); stopHorizontalMovement(); }
    public void stopVerticalMovement()   { m_velocity.y = 0.0f; }
    public void stopHorizontalMovement() { m_velocity.x = 0.0f; }


    private void AllowMovement()
    {
        if( m_isBright)
        {
            if (GameObject.FindObjectOfType<MapGrid>().getCellValue(m_position) / 1000 == 0
               || GameObject.FindObjectOfType<MapGrid>().getCellValue(m_position) / 1000 == 2)
                transform.position = m_position;
            else
                m_position = transform.position;
        }
        else if( m_isDark)
        {
            if (GameObject.FindObjectOfType<MapGrid>().getCellValue(m_position) / 1000 == 0
               || GameObject.FindObjectOfType<MapGrid>().getCellValue(m_position) / 1000 == 3)
                transform.position = m_position;
            else
                m_position = transform.position;
        }
        else if( !m_isDark || !m_isBright)
        {
            if (GameObject.FindObjectOfType<MapGrid>().getCellValue(m_position) / 1000 == 0)
                transform.position = m_position;
            else
                m_position = transform.position;
        }

        //Vector2 m_positionCellCoord = GameObject.FindObjectOfType<MapGrid>().getCellCoord(m_position);
        MapGrid map = GameObject.FindObjectOfType<MapGrid>();
        float offset = 0.1f;

        if (m_position.x < map.xMin )
            m_position.x = map.xMin + offset;

        if (m_position.x >= map.xMax)
            m_position.x = map.xMax - offset;

        if (m_position.y < map.yMin)
            m_position.y = map.yMin + offset;

        if (m_position.y >= map.yMax)
            m_position.y = map.yMax - offset;


    }
}

using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Vector2 m_position;
    private Vector2 m_velocity;

    private uint m_spriteDirection;

    private string m_gameColor;

	// Use this for initialization
	void Start () {

        m_spriteDirection = Constants.DOWN;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if (m_velocity != Vector2.zero)
            m_position += m_velocity * Time.deltaTime;

	}

    public Vector2 getPosition()        { return m_position; }
    public Vector2 getVelocity()        { return m_velocity; }
    public uint getSpriteDirection()    { return m_spriteDirection; }
    public string getGameColor()        { return m_gameColor; }

    public void setPosition(Vector2 position)               { m_position = position; }
    public void setVelocity(Vector2 velocity)               { m_velocity = velocity; }
    public void setSpriteDirection(uint spriteDirection)    { m_spriteDirection = spriteDirection; }
    public void setGameColor(string gameColor)              { m_gameColor = gameColor; }

}

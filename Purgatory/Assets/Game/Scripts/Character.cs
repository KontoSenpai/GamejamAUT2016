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
	void FixedUpdate () {
	
	}

    public Vector2 getPosition()        { return m_position; }
    public Vector2 getVelocity()        { return m_velocity; }
    public uint getSpriteDirection()    { return m_spriteDirection; }
    public string getGameColor()        { return m_gameColor; }
}

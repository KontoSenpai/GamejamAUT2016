using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Vector2 m_position;
    private Vector2 m_direction;

    private byte m_isDemon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

    public Vector2 getPosition() { return m_position; }
    public Vector2 getDirection() { return m_isDemon; }
}

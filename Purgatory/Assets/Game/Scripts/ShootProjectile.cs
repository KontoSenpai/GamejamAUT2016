using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	public float speed;

	private Vector3 mousePos;
	private int length;
	// Use this for initialization
	void Start () {
		length = 10;
	}

	void Update() {

		//transform.position += velocity * speed * Time.deltaTime;

		float step =  speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, mousePos * length, step);
	}


	public void SetMousePosition(Vector3 mouse) {
		mousePos = mouse;
	}
}

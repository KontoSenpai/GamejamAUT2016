using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	public float speed;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
	}

	void Update() {

		transform.position += velocity * speed * Time.deltaTime;
	}

	public void setVelocity(Vector2 velocity) {
		this.velocity = velocity;
	}
}

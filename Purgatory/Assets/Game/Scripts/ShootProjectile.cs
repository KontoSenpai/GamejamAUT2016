using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	public float speed;

	private Vector3 targetPos;

    //Increase the direction of the projectile 
    public uint length;

	// Use this for initialization
	void Start () {
	}

	void Update() {

		float step =  speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPos * length, step);
	}

    // Set the position to aim
    public void SetTargetPosition(Vector3 target) {
		targetPos = target;
	}
}

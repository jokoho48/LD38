using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public int speed;
    public float rotationSpeed;
    public GameObject player;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate() {
        Vector3 moveDirection = Vector3.Normalize(this.player.transform.position - this.transform.position);
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
        transform.LookAt(this.player.transform);
    }
}

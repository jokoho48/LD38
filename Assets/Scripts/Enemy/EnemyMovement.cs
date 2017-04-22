using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {
    public int speed;
    public float rotationSpeed;
    public GameObject player;

    Rigidbody rb;


	void Start () {
        rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
    }
	
	void FixedUpdate() {
        Vector3 moveDirection = Vector3.Normalize(this.player.transform.position - this.transform.position);
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime * speed);

        transform.rotation = Quaternion.LookRotation(moveDirection, PlayerController.GravObj.transform.up);
	}
}

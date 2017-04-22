using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {


	public float gravity;

	void Start()
	{
		if (gravity == 0)
			gravity = this.transform.localScale.x + this.transform.localScale.x / 2;
	}

	public void UpdatePhysics(Transform t, Rigidbody r, bool isGrounded, PlayerController player)
	{
		if (player.hasExitPlanetGravity)
			return;
		Vector3 objUp = t.up;
		Vector3 gravityUp = (t.position - transform.position).normalized;

		if (player.isJumping)
			r.AddForce(gravityUp * gravity);
		else
			r.AddForce(gravityUp * -gravity);


		r.drag = (isGrounded) ? 1f : 0.1f;

		// Orient relatived to gravity
		var q = Quaternion.FromToRotation(objUp, gravityUp);
		q = q * t.rotation;
		t.rotation = Quaternion.Slerp(t.rotation, q, 0.1f);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, gravity);
	}
}

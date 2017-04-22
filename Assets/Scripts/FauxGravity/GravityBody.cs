using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

	public float PlanetSize;
	public float PlanetGravity;
	public PlayerController player;

	void Start()
	{
		if (PlanetSize == 0)
			PlanetSize = this.transform.localScale.x;
		if (PlanetGravity == 0)
			PlanetGravity = PlanetSize / 2;
	}

	public void UpdatePhysics(Transform t, Rigidbody r, bool isGrounded)
	{
		if (player.hasExitPlanetGravity)
			return;
		Vector3 objUp = t.up;
		Vector3 gravityUp = (t.position - transform.position).normalized;

		if (player.isJumping)
			r.AddForce(gravityUp * PlanetGravity);
		else
			r.AddForce(gravityUp * -PlanetGravity);


		r.drag = (isGrounded) ? 1f : 0.1f;

		// Orient relatived to gravity
		var q = Quaternion.FromToRotation(objUp, gravityUp);
		q = q * t.rotation;
		t.rotation = Quaternion.Slerp(t.rotation, q, 0.1f);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, PlanetGravity + PlanetSize);
	}
}

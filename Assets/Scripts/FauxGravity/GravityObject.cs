using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour {

	private Rigidbody rb;
	private bool isGrounded = false;
	public GravityBody currentGravityBody;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.WakeUp();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate()
	{
		currentGravityBody.UpdatePhysics(this.transform, rb, isGrounded);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Gravity.groundLayerName))
		{
			isGrounded = true;
			currentGravityBody = collision.gameObject.GetComponent<GravityBody>();
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Gravity.groundLayerName) && isGrounded)
		{
			isGrounded = false;
		}
	}
}

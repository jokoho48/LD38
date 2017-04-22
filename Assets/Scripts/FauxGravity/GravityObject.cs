using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour {

	private Rigidbody rb;
	private bool isGrounded = false;



	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.WakeUp();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate()
	{
		Gravity.UpdatePhysics(this.transform, rb, isGrounded);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Gravity.groundLayerName))
		{
			isGrounded = true;
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

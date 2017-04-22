using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	GravityObject GravObj;
	Vector3 moveDir;
	public float moveSpeed = 15;
	public bool isJumping = false;
	public bool hasExitPlanetGravity = false;
	private float StartTime;
	public float JumpTime = 10;
	private Rigidbody rb;
	private void Awake()
	{
		GravObj = GetComponent<GravityObject>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			if (GravObj.isGrounded)
			{
				StartTime = Time.time;
			}
		}

		if (Input.GetButtonUp("Jump") || StartTime + JumpTime > Time.time)
			isJumping = false;

		if (hasExitPlanetGravity)
		{
			RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, 1000, 60 * Time.deltaTime);
			RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 1500, 60 * Time.deltaTime);
		}
		else
		{
			RenderSettings.fogStartDistance = 50;
			RenderSettings.fogEndDistance = 60;
		}
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
	}

}

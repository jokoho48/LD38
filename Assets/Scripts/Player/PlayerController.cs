using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static GravityObject GravObj;
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isJumping = true;
			if (GravObj.isGrounded)
			{
				StartTime = Time.time;
			}
		}

		if (Input.GetKeyUp(KeyCode.Space) || StartTime + JumpTime > Time.time)
			isJumping = false;
	}


	
	void FixedUpdate()
	{
		rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);

		// TODO: add Smooth Transtion
		if (hasExitPlanetGravity)
		{
			if (RenderSettings.fogStartDistance != 500)
			{
				RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, 500, 1200);
				RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 600, 1200);
			}
			
		}
		else
		{
			if (RenderSettings.fogStartDistance != 50)
			{
				RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, 50, 1200);
				RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 60, 1200);
			}
			
		}
		// Debug.Log("Fog: " + RenderSettings.fogEndDistance.ToString() + RenderSettings.fogStartDistance.ToString());
	}

}

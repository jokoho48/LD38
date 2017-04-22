using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

	public static string groundLayerName = "Ground";

	public static List<GravityBody> bodys = new List<GravityBody>();

	public static void RegisterGravityBody(GravityBody body)
	{
		bodys.Add(body);
	}

	public static void UnregisterGravityBody(GravityBody body)
	{
		bodys.Remove(body);
		CleanupGravityBodys();
	}

	private static void CleanupGravityBodys()
	{
		while (bodys.Contains(null))
		{
			bodys.Remove(null);
		}
	}

	public static void UpdatePhysics(Transform t, Rigidbody r, bool isGrounded)
	{

		Vector3 gravityUpGlobal = new Vector3();
		foreach (GravityBody gb in bodys)
		{
			Vector3 gravityUp;

			float distance = Vector3.Distance(t.position, gb.transform.position);
			if (distance <= gb.PlanetGravity)
			{
				gravityUp = t.position - gb.transform.position;
				gravityUp.Normalize();
				gravityUp = gravityUp * (-gb.PlanetGravity);
				gravityUpGlobal += gravityUp;
				r.AddForce(gravityUp);
			}
		}

		if (isGrounded)
		{
			r.drag = 1;
		}
		else
		{
			r.drag = 0.1f;
		}

		if (r.freezeRotation)
		{
			Vector3 gravityUp = new Vector3(gravityUpGlobal.x / bodys.Count, gravityUpGlobal.y / bodys.Count, gravityUpGlobal.z / bodys.Count);
			// Orient relatived to gravity
			Vector3 localUp = gravityUpGlobal.normalized;
			var q = Quaternion.FromToRotation(t.up, gravityUp);
			q = q * t.rotation;
			t.rotation = Quaternion.Slerp(t.rotation, q, 0.1f);
		}
	}
}
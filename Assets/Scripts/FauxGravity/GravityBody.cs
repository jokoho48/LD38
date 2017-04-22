using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

	public float PlanetSize;
	public float PlanetGravity;

	void Start()
	{
		if (PlanetSize == 0)
			PlanetSize = this.transform.localScale.x;
		if (PlanetGravity == 0)
			PlanetGravity = PlanetSize * 2;
	}

	void Awake()
	{
		Gravity.RegisterGravityBody(this);
	}

	void OnEnable()
	{
		Gravity.RegisterGravityBody(this);
	}

	void OnDestroy()
	{
		Gravity.UnregisterGravityBody(this);
	}

	void OnDisable()
	{
		Gravity.UnregisterGravityBody(this);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, PlanetGravity + PlanetSize);
	}
}

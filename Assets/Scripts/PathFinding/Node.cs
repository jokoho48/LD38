using UnityEngine;
using System.Collections;

public class Node
{
	public bool walkable;
	public Vector3 worldPosition;
    public Vector3 gridPosition;

	public float gCost;
	public float hCost;
    public Node parent;

	public Node(bool walkable, Vector3 worldPos, Vector3 gridPosition)
	{
		this.walkable = walkable;
		this.worldPosition = worldPos;
        this.gridPosition = gridPosition;
	}

	public float fCost {
		get	{ return this.gCost + this.hCost; }
	}
}

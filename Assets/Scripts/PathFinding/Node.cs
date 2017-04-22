using UnityEngine;
using System.Collections;
using System;

public class Node : IComparable<Node> {
	public bool walkable;
	public Vector3 worldPosition;
    public Vector3 gridPosition;

	public float gCost;
	public float hCost;
    public Node parent;

    public int heapIndex;

	public Node(bool walkable, Vector3 worldPos, Vector3 gridPosition)
	{
		this.walkable = walkable;
		this.worldPosition = worldPos;
        this.gridPosition = gridPosition;
	}

	public float fCost {
		get	{ return this.gCost + this.hCost; }
	}

    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}

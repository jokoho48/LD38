using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	Node[,,] grid;
    public bool displayGridGizmos;

    float nodeDiameter;
	int gridSizeX, gridSizeY, gridSizeZ;

	void Start() {
        this.nodeDiameter = this.nodeRadius * 2;
        this.gridSizeX = Mathf.RoundToInt(this.gridWorldSize.x / this.nodeDiameter);
        this.gridSizeY = Mathf.RoundToInt(this.gridWorldSize.y / this.nodeDiameter);
        this.gridSizeZ = Mathf.RoundToInt(this.gridWorldSize.z / this.nodeDiameter);
        this.CreateGrid();
	}

    public int MaxSize {
        get {
            return this.gridSizeX * this.gridSizeY * this.gridSizeZ;
        }
    }

	void CreateGrid() {
        this.grid = new Node[this.gridSizeX, this.gridSizeY, this.gridSizeZ];
		Vector3 worldBottomLeft = this.transform.position - (Vector3.right * this.gridWorldSize.x / 2) - (Vector3.forward * this.gridWorldSize.y / 2) - (Vector3.up * this.gridWorldSize.z / 2);

		for (int x = 0; x < this.gridSizeX; x++) {
            Vector3 xPos = Vector3.right * (x * this.nodeDiameter + this.nodeRadius);
            for (int y = 0; y < this.gridSizeY; y++) {
                Vector3 yPos = Vector3.forward * (y * this.nodeDiameter + this.nodeRadius);
                for (int z = 0; z < this.gridSizeZ; z++) {
                    Vector3 zPos = Vector3.up * (z * this.nodeDiameter + this.nodeRadius);
                    Vector3 worldPoint = worldBottomLeft + xPos + yPos + zPos;
					bool walkable = !(Physics.CheckSphere(worldPoint, this.nodeRadius, this.unwalkableMask));
                    this.grid[x, y, z] = new Node(walkable, worldPoint, new Vector3(x, y, z));
				}
			}
		}
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				for (int z = -1; z <= 1; z++) {
					if (x == 0 && y == 0 && z == 0)
						continue;

					int checkX = (int)node.gridPosition.x + x;
					int checkY = (int)node.gridPosition.y + y;
					int checkZ = (int)node.gridPosition.z + z;
					if (checkX >= 0 && checkX < this.gridSizeX && checkY >= 0 && checkY < this.gridSizeY && checkZ >= 0 && checkZ < this.gridSizeZ) {
						neighbours.Add(this.grid[checkX, checkY, checkZ]);
					}
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + this.gridWorldSize.x / 2) / this.gridWorldSize.x;
		float percentY = (worldPosition.z + this.gridWorldSize.y / 2) / this.gridWorldSize.y;
		float percentZ = (worldPosition.y + this.gridWorldSize.z / 2) / this.gridWorldSize.z;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		percentZ = Mathf.Clamp01(percentZ);

		int x = Mathf.RoundToInt((this.gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((this.gridSizeY - 1) * percentY);
		int z = Mathf.RoundToInt((this.gridSizeZ - 1) * percentZ);
		return grid[x, y, z];
	}

    public List<Node> path;
	void OnDrawGizmos()	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(this.gridWorldSize.x, this.gridWorldSize.z, this.gridWorldSize.y));

	    if (this.displayGridGizmos && this.grid != null) {
			foreach (Node node in this.grid) {
                if (!node.walkable) {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (this.nodeDiameter - .1f));
                }
			}
            if (this.path != null) {
                foreach (Node node in this.path) {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (this.nodeDiameter - .1f));
                }
            }
		}
	}
}
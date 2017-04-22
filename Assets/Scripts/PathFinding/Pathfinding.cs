using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    public Transform seeker, target;
    public Grid grid;

    void Update() {
        if (Input.GetButtonDown("Jump"))
            this.FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos) {
        Node startNode = this.grid.NodeFromWorldPoint(startPos);
        Node targetNode = this.grid.NodeFromWorldPoint(targetPos);

        NodeHeap openSet = new NodeHeap(this.grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node currentNode = openSet.RemoveFirst();

            closedSet.Add(currentNode);

            if (currentNode == targetNode) {
                this.RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in this.grid.GetNeighbours(currentNode)) {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                float newMovementCostToNeighbour = currentNode.gCost + this.GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = this.GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        this.grid.path = path;
    }

    float GetDistance(Node nodeA, Node nodeB) {
        return Vector3.Distance(nodeA.gridPosition, nodeB.gridPosition);
    }
}

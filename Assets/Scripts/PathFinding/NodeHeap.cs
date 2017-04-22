using UnityEngine;
using System;
using System.Collections;

public class NodeHeap {
	Node[] nodes;
	int currentItemCount;

	public NodeHeap(int maxHeapSize) {
		this.nodes = new Node[maxHeapSize];
	}

	public void Add(Node node)	{
        node.heapIndex = this.currentItemCount;
		this.nodes[this.currentItemCount] = node;
        this.SortUp(node);
        this.currentItemCount++;
	}

	public Node RemoveFirst() {
		Node firstNode = nodes[0];
        this.currentItemCount--;
        this.nodes[0] = nodes[this.currentItemCount];
        this.nodes[0].heapIndex = 0;
		this.SortDown(this.nodes[0]);
		return firstNode;
	}

    public void UpdateItem(Node node) {
        this.SortUp(node);
    }

    public int Count {
        get { return this.currentItemCount; }
    }

    public bool Contains(Node node) {
        return object.Equals(this.nodes[node.heapIndex], node);
    }

	void SortDown(Node node) {
		while (true) {
			int childIndexLeft = node.heapIndex * 2 + 1;
			int childIndexRight = node.heapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < this.currentItemCount) {
				swapIndex = childIndexLeft;

				if (childIndexRight < this.currentItemCount)	{
					if (this.nodes[childIndexLeft].CompareTo(this.nodes[childIndexRight]) < 0)
						swapIndex = childIndexRight;
				}

				if (node.CompareTo(this.nodes[swapIndex]) < 0)
                    this.Swap(node, this.nodes[swapIndex]);
				else
					return;
			}
			else
				return;
		}
	}

	void SortUp(Node node) {
		int parentIndex = (node.heapIndex - 1) / 2;

		while (true) {
			Node parentNode = nodes[parentIndex];
			if (node.CompareTo(parentNode) > 0) {
                this.Swap(node, parentNode);
			} else {
				break;
			}
		}
	}

	void Swap(Node nodeA, Node nodeB)	{
        this.nodes[nodeA.heapIndex] = nodeB;
        this.nodes[nodeB.heapIndex] = nodeA;
		int tmp = nodeA.heapIndex;
        nodeA.heapIndex = nodeB.heapIndex;
        nodeB.heapIndex = tmp;
	}
}
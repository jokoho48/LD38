using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    public GameObject player;

    Rigidbody rb;
    Vector3 target;


	void Start () {
        this.rb = GetComponent<Rigidbody>();
        this.target = player.transform.position;
    }

    void Update() {
        if (this.rb.velocity != Vector3.zero && Physics.Raycast(this.transform.position, this.rb.velocity, out this.hit, 100f)) {
            if (this.hit.collider.gameObject.tag != "Player")
                this.target = this.hit.point + this.hit.normal * 5;
        }
        if (Vector3.Distance(this.transform.position, this.target) < 1) {
            this.target = player.transform.position;
        }
    }
	
	void FixedUpdate() {
        if (Vector3.Distance(this.transform.position, this.player.transform.position) > 4) {
            this.rb.velocity = Vector3.MoveTowards(this.rb.velocity, Vector3.Normalize(this.target - this.transform.position) * this.speed, 0.1f);
        }
        
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(this.rb.velocity, PlayerController.GravObj.transform.up), this.rotationSpeed);
	}

    RaycastHit hit;
    private void OnDrawGizmos() {
        if (this.hit.point != Vector3.zero) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, hit.point);
            Gizmos.DrawLine(hit.point, hit.point + hit.normal * 10);
        }
        if (this.rb) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.rb.velocity * this.speed);
        }
    }
}

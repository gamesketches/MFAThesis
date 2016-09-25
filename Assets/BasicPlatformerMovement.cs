using UnityEngine;
using System.Collections;

public class BasicPlatformerMovement : MonoBehaviour {

	Rigidbody2D rb;
	Animator animator;
	public float jumpPower;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float hori = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Jump");
		if(hori != 0) {
			animator.SetInteger("movement", 1);
			transform.Translate(hori * Time.deltaTime, 0, 0);

		}
		else {
			animator.SetInteger("movement", 0);
		}
		if(vert != 0 && animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
		if(!animator.GetBool("grounded")) {
			if(rb.velocity.y == 0) {
				animator.SetBool("grounded", true);
			}
		}
	}
}

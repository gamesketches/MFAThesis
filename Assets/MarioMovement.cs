using UnityEngine;
using System.Collections;

public class MarioMovement : MonoBehaviour {

	Animator animationController;
	Rigidbody2D rb;
	SpriteRenderer renderer;
	float distanceTraveled;
	bool turnTheBeatAround;
	// Use this for initialization
	void Start () {
		animationController = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		distanceTraveled = 0;
		turnTheBeatAround = false;
		renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float hori = Input.GetAxis("Horizontal");
		float jump = Input.GetAxis("Jump");
		if(rb.velocity.x > 5) {
			rb.AddForce(new Vector2(hori, jump), ForceMode2D.Impulse);
		}
		animationController.SetInteger("movement", Mathf.CeilToInt(hori));
		animationController.SetBool("grounded", jump == 0);
		distanceTraveled += hori;
		if(distanceTraveled > 300) {
			turnTheBeatAround = true;
		}
	 	renderer.flipX = FlipSprite();
	}

	bool FlipSprite(){
		if(turnTheBeatAround) {
			return rb.velocity.x > 0 ? false : true;
		}
		else {
			return rb.velocity.x > 0 ? true : false;
		}
	}
}

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
		Debug.Log(rb.velocity.x);
		float hori = Input.GetAxis("Horizontal");
		float jump = Input.GetAxis("Jump");
		if(rb.velocity.x < 3 && rb.velocity.x > -3) {
			rb.AddForce(new Vector2(hori, 0f), ForceMode2D.Impulse);
		}
		if(jump != 0 && rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, jump * 5f), ForceMode2D.Impulse);
		}
		animationController.SetInteger("movement", Mathf.CeilToInt(hori));
		animationController.SetBool("grounded", jump == 0);
		distanceTraveled += hori;
		if(transform.position.x > 37 && !turnTheBeatAround) {
			turnTheBeatAround = true;
		}
	 	renderer.flipX = FlipSprite();
	 	renderer.flipY = turnTheBeatAround;
	}

	bool FlipSprite(){
		if(turnTheBeatAround) {
			return rb.velocity.x > 0.1f ? true : false;
		}
		else {
			return rb.velocity.x > 0.1f ? false : true;
		}
	}

}

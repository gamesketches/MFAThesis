using UnityEngine;
using System.Collections;

public class MenuBasedPlatformerMovement : MonoBehaviour {

	Rigidbody2D rb;
	Animator animator;
	public float jumpPower;
	public float speed;
	private float movementSpeed;
	private int direction;
	// Use this for initialization
	void Start () {
		direction = 1;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		movementSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
			animator.SetInteger("movement", (int)movementSpeed);
			transform.Translate(movementSpeed * speed * Time.deltaTime * direction, 0, 0);
		if(!animator.GetBool("grounded")) {
			if(rb.velocity.y == 0) {
				animator.SetBool("grounded", true);
			}
		}
	}

	public void Forward() {
		movementSpeed = 1;
	}

	public void Back() {
		movementSpeed = -1;
	}

	public void Turn() {
		direction *= -1;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.flipX = !renderer.flipX;
	}

	public void Stop() {
		movementSpeed = 0;
	}

	public void Jump() {
		if(animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
	}

	public void Hop() {
		if(animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			rb.AddForce(new Vector2(0, jumpPower / 2), ForceMode2D.Impulse);
		}
	}

	public void High() {
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		AttackHitBox.transform.position += new Vector3(1, 1, 0);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}

	public void Middle() {
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}

	public void Low() {
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		AttackHitBox.transform.position += new Vector3(1, -1, 0);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}


	IEnumerator ManageAttackHitBox(GameObject attack) {
		yield return new WaitForSeconds(1);
		Destroy(attack);
	}
}

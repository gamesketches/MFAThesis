using UnityEngine;
using System.Collections;

public class MenuBasedPlatformerMovement : MonoBehaviour {

	private enum AttackType {Neutral, Low, Mid, High};
	Rigidbody2D rb;
	Animator animator;
	public float jumpPower;
	public float speed;
	private float movementSpeed;
	private int direction;
	private bool doubleJumped;
	public bool blocking;
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
			Vector3 playerPos = transform.position;
			transform.Translate(movementSpeed * speed * Time.deltaTime * direction, 0, 0);
		if(!animator.GetBool("grounded")) {
			if(rb.velocity.y == 0) {
				animator.SetBool("grounded", true);
				doubleJumped = false;
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

	public void Slide() {
		movementSpeed = 0;
		transform.rotation = Quaternion.Euler(0, 0, 90);
		Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
		StartCoroutine(Sliding());
	}

	public void Dash() {
		rb.AddForce(new Vector2(6, 1), ForceMode2D.Impulse);
	}

	public void Block() {
		StartCoroutine(StartBlocking());
	}

	IEnumerator StartBlocking() {
		blocking = true;
		yield return new WaitForSeconds(1);
		blocking = false;
	}


	IEnumerator Sliding() {
		float t = 0;
		while(t < 0.5f) {
			transform.Translate(0, -4 * Time.deltaTime * direction, 0);
			Camera.main.transform.Translate(4 * Time.deltaTime * direction, 0, 0);
			t += Time.deltaTime;
			yield return null;
		}
		transform.rotation = Quaternion.identity;
		Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
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
			rb.AddForce(new Vector2(2 * direction, jumpPower / 2), ForceMode2D.Impulse);
		}
	}
	public void SuperJump() {
		if(animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			rb.AddForce(new Vector2(0, jumpPower * 1.7f), ForceMode2D.Impulse);
		}
	}

	public void DoubleJump() {
		if(!animator.GetBool("grounded") && !doubleJumped) {
			doubleJumped = true;
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
	}

	public void AirDash() {
		if(!animator.GetBool("grounded") && !doubleJumped) {
			doubleJumped = true;
			Debug.Log("dashin");
			rb.AddForce(new Vector2(4, 1), ForceMode2D.Impulse);
		}
	}

	public void SlideAttack() {
		Middle();
		Slide();
	}

	public void StudderStep() {
		StartCoroutine(DoStudderStep());
	}

	IEnumerator DoStudderStep() {
		if(animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			rb.AddForce(new Vector2(3 * -direction, jumpPower / 3), ForceMode2D.Impulse);
			while(!animator.GetBool("grounded")) {
				yield return null;
			}
			Thrust();
		}
	}

	public void Thrust() {
		Middle();
		Dash();
	}

	public void JumpSlash() {
		if(animator.GetBool("grounded")) {
			animator.SetBool("grounded", false);
			Low();
			ApplyMovementForces(5 * direction, jumpPower);
		}
	}

	public void SpinAttack() {
		JumpSlash();
		StartCoroutine(SpinPlayer());
	}

	IEnumerator SpinPlayer() {
		while(!animator.GetBool("grounded")) {
			transform.Rotate(Vector3.forward * 5);
			Camera.main.transform.rotation = Quaternion.identity;
			yield return null;
		}
		transform.rotation = Quaternion.identity;
		Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	public void High() {
		animator.SetInteger("attacking", (int)AttackType.High);
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		AttackHitBox.transform.position += new Vector3(1, 1, 0);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}

	public void Middle() {
		animator.SetInteger("attacking", (int)AttackType.Mid);
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}

	public void Low() {
		animator.SetInteger("attacking", (int)AttackType.Low);
		GameObject AttackHitBox = (GameObject)Instantiate(Resources.Load<GameObject>("prefabs/AttackHitBox"), transform.position, Quaternion.identity);
		AttackHitBox.transform.position += new Vector3(1, -1, 0);
		StartCoroutine(ManageAttackHitBox(AttackHitBox));
	}


	IEnumerator ManageAttackHitBox(GameObject attack) {
		attack.transform.SetParent(transform);
		yield return new WaitForSeconds(1);
		Destroy(attack);

		animator.SetInteger("attacking", (int)AttackType.Neutral);
	}

	public void PrintTest() {
		Debug.Log("The test worked");
	}

	void ApplyMovementForces(float x, float y) {
		rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
	}
}

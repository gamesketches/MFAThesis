using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AcrobaticPlatformer : MonoBehaviour {

	Rigidbody2D rb;
	Animator animator;
	SpriteRenderer renderer;
	public float jumpPower;
	public float speed;
	public bool wallCling;
	public float freezeTime;
	public float noiseLimit;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		wallCling = false;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(mic.MicLoudness > noiseLimit && !restarting) {
			StartCoroutine(DeathFreeze());
		}*/
		if(wallCling) {
			float vert = Input.GetAxis("Jump");
			Debug.Log("wall cling!");
			rb.gravityScale = 0;
			rb.velocity = Vector2.zero;
			if(vert != 0) {
				wallCling = false;
				rb.gravityScale = 1;
				rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			}
		}
		else {
			float hori = Input.GetAxis("Horizontal");
			float vert = Input.GetAxis("Jump");
			if(hori != 0) {
				if(hori < 0 && !renderer.flipX) {
					renderer.flipX = true;
				}
				else if(hori > 0 && renderer.flipX) {
					renderer.flipX = false;
				}
				animator.SetInteger("movement", 1);
				transform.Translate(hori * speed * Time.deltaTime, 0, 0);
	
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

	IEnumerator DeathFreeze() {
		if(!audioSource.isPlaying) {
		audioSource.Play();
		}
		float startTime = Time.realtimeSinceStartup;
		Time.timeScale = 0;
		while(Time.realtimeSinceStartup < startTime + freezeTime) {
			yield return null;
		}
		Time.timeScale = 1;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void PrintTest() {
		Debug.Log("printin");
	}
}

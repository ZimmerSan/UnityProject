using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public static HeroRabbit lastRabbit = null;

	public AudioClip walkAudio, dieAudio;
	public AudioSource walkAudioSource, dieAudioSource;

	public float speed = 1;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;
	public float deathTotalTime = 1f;

	bool isGrounded = false;
	bool jumpActive = false;
	float timeToDeath = -1000f;
	float jumpTime = 0f;
	float deathTime;
	bool isRambo = false;
	bool isDead = false;

	Transform heroParent = null;
	SpriteRenderer spriteRenderer = null;
	Rigidbody2D myBody = null;
	Animator animator = null;

	void Awake() {
		lastRabbit = this;

		walkAudioSource = gameObject.AddComponent<AudioSource>();
		dieAudioSource = gameObject.AddComponent<AudioSource>();

		walkAudioSource.clip = walkAudio;
		dieAudioSource.clip = dieAudio;
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		myBody = this.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
		heroParent = this.transform.parent;
		deathTime = deathTotalTime;
		LevelController.current.setStartPosition (transform.position);
	}

	void FixedUpdate () {

		//Handle horizontal movement
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			if (!walkAudioSource.isPlaying && SoundManager.Instance.isSoundOn()) walkAudioSource.Play ();
			animator.SetBool ("run", true);

			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;

			if (value < 0)		spriteRenderer.flipX = true;
			else if (value > 0)	spriteRenderer.flipX = false;
		} else {
			if (walkAudioSource.isPlaying) walkAudioSource.Pause ();
			animator.SetBool ("run", false);
		}

		// RayCast bounds
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layerId = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast (from, to, layerId);
		Debug.DrawLine (from, to, Color.red);

		if (hit) {
			isGrounded = true;
			//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				setNewParent(hit.transform);
			}
		} else {
			isGrounded = false;
			setNewParent (heroParent);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) this.jumpActive = true;

		if (this.jumpActive) {
			// Button is still held
			if (Input.GetButton ("Jump")) {
				this.jumpTime += Time.deltaTime;
				if (this.jumpTime < this.maxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.jumpActive = false;
				this.jumpTime = 0;
			}
		}

		if (this.isGrounded)	animator.SetBool ("jump", false);
		else 					animator.SetBool ("jump", true);

		if (isDead) {
			timeToDeath -= Time.deltaTime;
			if (timeToDeath <= 0) {
				LevelController.current.onRabbitDeath (this);
				isDead = false;
				animator.SetBool ("die", false);
			}
		}

	}

	void setNewParent(Transform newParent) {
		if (this.transform.parent != newParent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = this.transform.position;

			//Встановлюємо нового батька
			this.transform.parent = newParent;

			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			this.transform.position = pos;
		}
	}

	public bool beRambo(bool really) {
		if (really && !this.isRambo) {
			this.isRambo = true;
			this.transform.localScale += new Vector3 (0.5f, 0.5f, 0);
			return true;
		} else if (!really && this.isRambo) {
			this.isRambo = false;
			this.transform.localScale -= new Vector3 (0.5f, 0.5f, 0);
			return true;
		}
		return false;
	}

	public bool isTheRambo() { return isRambo; }

	public void die (Transform killer) {
		if (killer != null) {
			animator.SetBool ("die", true);
			timeToDeath = deathTotalTime;
		} else {
			timeToDeath = 0;
		}
		if (SoundManager.Instance.isSoundOn()) dieAudioSource.Play ();
		isDead = true;
	}
}

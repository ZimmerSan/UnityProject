using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : Orc {

	public float cooldown = 5;
	public float normalSpeed = 1;
	public float attackSpeed = 2;

	float currentCooldown;

	// Use this for initialization
	void Start () {
		base.Start ();
		speed = normalSpeed;
		currentCooldown = 0;
	}

	protected override void checkSetAttackMode () {
		if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
		else base.checkSetAttackMode ();
	}

	protected override bool isRabbitReachable() {
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;
		return rabbit_pos.x >= pointA.x && rabbit_pos.x <= pointB.x && Mathf.Abs(rabbit_pos.y - my_pos.y) < 3f;
	}

	protected override void attack() {
		if (mode == Mode.Attack && currentCooldown <= 0) {
			speed = attackSpeed;
			animator.SetBool ("walk", false);
			animator.SetBool ("run", true);


			if (isRabbitClose ()) {
				Debug.Log ("Is close");
				StartCoroutine (hitRabbit ());
				if (isRabbitDomineering ()) {
					Debug.Log ("Is domineering");
					mode = Mode.Die;
					StartCoroutine (die ());
				} else {
					StartCoroutine (killRabbit ());
				}
			}

		} else {
			speed = normalSpeed;
			animator.SetBool ("walk", true);
			animator.SetBool ("run", false);
			animator.SetBool ("attack", false);
		}
	}

	IEnumerator killRabbit() {
		yield return new WaitForSeconds(0.5f);
		if (currentCooldown <= 0) {
			HeroRabbit rabbit = HeroRabbit.lastRabbit;
			if (rabbit.isTheRambo ()) {
				rabbit.beRambo (false);
			} else {
				HeroRabbit.lastRabbit.die (this.transform);
			}
			currentCooldown = cooldown;
			if (sr.flipX) mode = Mode.GoToA;
			else mode = Mode.GoToB;
		}
	}

	IEnumerator hitRabbit() { 
		animator.SetBool ("attack", true);
		animator.SetBool ("walk", false);
		animator.SetBool ("run", false);
		if (SoundManager.Instance.isSoundOn()) this.attackSoundSource.Play ();
		yield return new WaitForSeconds(1f);
	}

}

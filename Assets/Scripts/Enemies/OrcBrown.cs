using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBrown : Orc {

	public float normalSpeed = 1;

	public GameObject prefabCarrot;
	public float carrot_radius = 5.0f;
	float last_carrot = 0;
	public float carrotIntervalSeconds = 5.0f;

	// Use this for initialization
	void Start () {
		base.Start ();
		speed = normalSpeed;
	}

	void launchCarrot(float direction) {
		if (Time.time - last_carrot > carrotIntervalSeconds) {
			last_carrot = Time.time;
			animator.SetTrigger ("attack");

			GameObject obj = GameObject.Instantiate(this.prefabCarrot);
			Vector3 carrot_pos = this.transform.position;
			carrot_pos.y += 1;
			obj.transform.position = carrot_pos;

			Carrot carrot = obj.GetComponent<Carrot>();
			carrot.launch(direction);

		}
	}

	protected override void checkSetAttackMode () {
		if (isRabbitReachable ())
			mode = Mode.Attack;
		else if (sr.flipX)
			mode = Mode.GoToB;
		else
			mode = Mode.GoToA;
	}

	protected override bool isRabbitReachable () {
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;

		return Mathf.Abs(rabbit_pos.x - my_pos.x) < carrot_radius;
	}

	public override void move () {
		if (mode == Mode.Attack) {
			speed = 0;
		} else {
			speed = normalSpeed;
		}
		base.move ();
	}

	protected override void attack() {
		if (mode == Mode.Attack) {
			Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
			Vector3 my_pos = this.transform.position;

			launchCarrot (rabbit_pos.x - my_pos.x);
			if (isRabbitClose () && isRabbitDomineering ()) {
				mode = Mode.Die;
				StartCoroutine (die ());
			}
		} else {
			speed = normalSpeed;
		}
	}
}

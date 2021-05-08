using System.Collections;
using UnityEngine;

public class ChargeInDarkness : Attack
{
	public GameObject levelController;

	public GameObject lightningBall;
	private ChargeLightningOrb ballInstance;

	public GameObject lightningStrike;

	[Header("Charges")]
	public float arenaRadius;
	public int numberOfCharges;
	public float chargeSpeed;
	public float timeBetweenCharges = 2;
	public float timeBetweenChargesFactor = 1;
	public float endPosAngle = 90;

	[Header("Attack conditions")]
	public float healthTriggerPercentage;

	private Vector2 lastChargePosition = Vector2.zero;
	Transform chargeTarget;

	Boss boss;
	Animator anim;
	Health health;
	new SpriteRenderer renderer;

	Coroutine attack;
	Coroutine ballMovement;

	bool registeredDeath = false;
	bool meleeDamageChanged = false;

	private void Start()
	{
		boss = GetComponent<Boss>();
		anim = GetComponent<Animator>();
		health = GetComponent<Health>();
		renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{

		if (!meleeDamageChanged && health.CurrentHealth <= health.maxHealth * healthTriggerPercentage)
		{
			boss.meleeDamageFactor = 0.2f;
			meleeDamageChanged = true;
		}

		if (health.CurrentHealth <= 0 && !registeredDeath)
		{
			if (ballMovement != null)
				StopCoroutine(ballMovement);
			StopCoroutine(attack);
			GetComponent<Death>().deathPosition = ballInstance.transform.position;
			Destroy(ballInstance.gameObject);
			boss.useCat = false;
			attackStage = AttackStage.Finished;
			registeredDeath = true;
		}
	}

	public override void StartAttack()
	{
		attack = StartCoroutine(Attack());
	}

	public IEnumerator Attack()
	{
		boss.meleeDamageFactor = 1;
		boss.canBeDamaged = false;
		// spawn the charge target
		chargeTarget = new GameObject("Charge Target").transform;
		chargeTarget.position = Vector3.right * arenaRadius;
		anim.SetTrigger("use_fish");
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_fish_flapping, 1.6f);
		yield return new WaitForSeconds(1.3f);
		soundEngine.play_sound(soundEngine.sound_type.boss_gulp);
		yield return new WaitForSeconds(0.3f);
		soundEngine.play_sound(soundEngine.sound_type.meow_angry);
		soundEngine.play_sound(soundEngine.sound_type.boss_orb_lightning);
		GameObject lightning = Instantiate(lightningStrike, transform.position + new Vector3(0, 30), Quaternion.AngleAxis(90, Vector3.forward));
		lightning.transform.localScale = new Vector3(30, 30, 1);
		Destroy(lightning, 0.25f);

		// move boss away, spawn ball
		transform.position = new Vector2(100, 100);
		attackStage = AttackStage.Attack;
		levelController.GetComponent<MakeLavaCold>().Activate();
		ballInstance = Instantiate(lightningBall, Vector2.zero, Quaternion.identity).GetComponent<ChargeLightningOrb>();
		ballInstance.boss = boss;

		yield return new WaitForSeconds(2);
		lastChargePosition = MoveChargeTargetToRandomEndPosition();  // einmal bewegen, damits ein bisschen zufälliger wird
		yield return StartCoroutine(ballInstance.MoveWithoutSound(Vector2.zero, lastChargePosition, chargeSpeed / 4));
		yield return new WaitForSeconds(timeBetweenCharges * timeBetweenChargesFactor);
		boss.canBeDamaged = true;

		for (int i = 0; i != numberOfCharges; i++)
		{
			Vector2 endPos = MoveChargeTargetToRandomEndPosition();
			yield return ballMovement = StartCoroutine(ballInstance.Move(lastChargePosition, endPos, chargeSpeed));
			ballMovement = null;
			lastChargePosition = chargeTarget.position;
			i++;
			yield return new WaitForSeconds(timeBetweenCharges * timeBetweenChargesFactor);
		}
	}

	Vector2 MoveChargeTargetToRandomEndPosition()
	{
		float angle = (Random.value * endPosAngle + (180 - endPosAngle / 2)) % 360;
		chargeTarget.RotateAround(Vector3.zero, Vector3.forward, angle);
		return chargeTarget.position;
	}

	public override int WeightAttack()
	{
		int value = 0;
		if (health.CurrentHealth <= health.maxHealth * healthTriggerPercentage)
			value = 101;

		return value;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 150, 75, 25), "Charge"))
		{
			StartAttack();
		}*/
	}
}

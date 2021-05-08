using System.Collections;
using UnityEngine;

public class Attack_PBAOE : Attack // Point Blank Area of Effect, direkt unter dem Boss
{
	// settings
	public float castTime = 1;	// muss mit der exit time der windup animation übereinstimmen
	public float loopTime = 0;
	public float downTime = 2;

	// shape
	public float radius = 1.2f;

	public LayerMask playerLayer;

	// indicator
	public GameObject indicatorPrefab;
	private GameObject indicatorInstance;
	private Indicator_PBAOE indicator;
	public GameObject particles;

	public float shakeDuration, shakeStrenght;
	public float knockBackDuration = 1;
	public float knockbackStrenght = 5000;
	public CameraShake shaker;

	Boss boss;
	Animator anim;

	private void Start()
	{
		boss = GetComponent<Boss>();
		anim = GetComponent<Animator>();
	}

	public override void StartAttack()
	{
		StartCoroutine("Smash");
	}

	IEnumerator Smash()
	{
		anim.SetTrigger("use_smash");
		yield return new WaitForSeconds(0.1f);
		SetupIndicator();
		yield return new WaitForSeconds(castTime);
		GameObject g = Instantiate(particles, Vector3.zero, Quaternion.AngleAxis(-150, Vector3.right));
		g.GetComponent<ParticleSystem>().Play();
		soundEngine.play_sound(soundEngine.sound_type.smash_pbaoe);
		shaker.Shake(shakeDuration, shakeStrenght);

		Collider2D playerHit = Physics2D.OverlapCircle(Vector3.zero, radius, playerLayer);
		if (playerHit != null)
		{
			popup_damage.popup(boss.damage_FallingRock,playerHit.transform.position, true);
			playerHit.transform.GetComponent<in_targetable>().take_damage(boss.damage_FallingRock);
			playerHit.transform.GetComponent<movement>().knockback(knockBackDuration, knockbackStrenght, transform);
		}

		attackStage = AttackStage.Downtime;
		yield return new WaitForSeconds(0.5f);

		attackStage = AttackStage.Finished;
		Destroy(g, 1);
	}

	private IEnumerator StartAttackSequence()
	{
		SetupIndicator();
		yield return new WaitForSeconds(castTime);
		SetupAttack();
		yield return new WaitForSeconds(loopTime);
		SetupDowntime();
		yield return new WaitForSeconds(downTime);
		SetupFinish();
	}

	private void SetupIndicator()
	{
		attackStage = AttackStage.Indicator;
		// Instantiate and spawn indicator, destroy when its finished
		indicatorInstance = Instantiate(indicatorPrefab);
		indicator = indicatorInstance.GetComponent<Indicator_PBAOE>();
		indicator.StartIndicator(radius, castTime);
		Destroy(indicatorInstance, castTime);
	}

	private void SetupAttack()
	{
		attackStage = AttackStage.Attack;
		// play animation
		//attackAnimationInstance = Instantiate(attackAnimationPrefab);
		//attackAnimationInstance.transform.localScale = new Vector3(radius, radius, 1);
		//attackAnimation = attackAnimationInstance.GetComponentInChildren<Animator>();
		//attackAnimation.SetTrigger("explosion");
	}

	private void SetupDowntime()
	{
		attackStage = AttackStage.Downtime;
	}

	private void SetupFinish()
	{
		//Destroy(attackAnimationInstance);
		attackStage = AttackStage.Finished;
	}

	public override int WeightAttack()
	{
		int value = 0;
		if (Vector3.Distance(boss.target.position, transform.position) < radius)
			value += 50;
		if (Vector3.Distance(boss.target.position, transform.position) < radius / 2)
			value += 50;

		return value;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 200, 75, 25), "SMASH"))
			StartAttack();
		*/
	}
}

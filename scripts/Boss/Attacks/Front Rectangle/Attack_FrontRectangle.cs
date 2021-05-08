using System.Collections;
using UnityEngine;

public class Attack_FrontRectangle : Attack
{
	// settings
	public float castTime = 1;
	public float loopTime = 0;
	public float downTime = 2;

	// shape
	public float length;
	public float width;

	// cast direction
	private Vector3 castDirection;

	// indicator
	public GameObject indicatorPrefab;
	private GameObject indicatorInstance;
	private Indicator_FrontRectangle indicator;

	// attack animation
	public GameObject attackAnimationPrefab;
	public float animationScaleFactor;
	private GameObject attackAnimationInstance;
	private Animator[] attackAnimation;

	private Animator anim;
	public GameObject stareEffect;

	public override void StartAttack()
	{
		anim = GetComponent<Animator>();
		StartCoroutine("StartAttackSequence");
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
		anim.SetTrigger("use_stare");
		GameObject g = Instantiate(stareEffect, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
		ParticleSystem p = g.GetComponent<ParticleSystem>();
		p.Play();
		Destroy(g, 1.0f);

		attackStage = AttackStage.Indicator;
		// Instantiate and spawn indicator, destroy when its finished
		castDirection = transform.right;
		indicatorInstance = Instantiate(indicatorPrefab);
		indicatorInstance.transform.right = -castDirection;
		indicator = indicatorInstance.GetComponent<Indicator_FrontRectangle>();
		indicator.StartIndicator(length, width, castTime);
		Destroy(indicatorInstance, castTime);
	}

	private void SetupAttack()
	{
		attackStage = AttackStage.Attack;
		// play animation
		attackAnimationInstance = Instantiate(attackAnimationPrefab);

		attackAnimationInstance.transform.right = castDirection;

		attackAnimationInstance.transform.localScale = new Vector3(length, width, 1);
		attackAnimation = attackAnimationInstance.GetComponentsInChildren<Animator>();
		//foreach(Animator a in attackAnimation)
		//	a.SetTrigger("explosion");
	}

	private void SetupDowntime()
	{
		attackStage = AttackStage.Downtime;
	}

	private void SetupFinish()
	{
		Destroy(attackAnimationInstance);
		attackStage = AttackStage.Finished;
	}
}

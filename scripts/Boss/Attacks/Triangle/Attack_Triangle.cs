using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Triangle : Attack
{

	public GameObject triangleIndicator;
	public float indicatorDuration = 1;
	public float clockwiseDelay = 0.3f;
	public float checkersDelay = 0.3f;
	public float targetPlayerDelay = 1.5f;

	Boss boss;
	
	private const float AREA_ANGLE = 60;

	private void Start()
	{
		boss = GetComponent<Boss>();
	}

	public override void StartAttack()
	{
		attackStage = AttackStage.Attack;
		StartCoroutine(Attack());
	}

	private void Update()
	{
		if (Vector3.Distance(boss.target.position, Vector3.zero) < 1.5f)
		{
			attackStage = AttackStage.Finished;
			StopAllCoroutines();
		}
	}

	private IEnumerator Attack()
	{
		int type = Mathf.FloorToInt(Random.value * 4);
		switch (type)
		{
			case 0:
				StartCoroutine(SideToSide());
				break;
			case 1:
				StartCoroutine(ClockWise());
				break;
			case 2:
				StartCoroutine(Checkers());
				break;
			case 3:
				StartCoroutine(TargetPlayer());
				break;
		}
		yield return new WaitForSeconds(5);
		attackStage = AttackStage.Finished;
	}

	private void CreateIndicator(float angle)
	{
		GameObject g = Instantiate(triangleIndicator, Vector3.zero, Quaternion.AngleAxis(angle, Vector3.forward));
		g.GetComponent<Indicator_Triangle>().StartIndicator(indicatorDuration, boss, angle);
	}

	private IEnumerator SideToSide()
	{
		float playerAngle = GetPlayerAngle();
		// spawn player side
		CreateIndicator(playerAngle);
		CreateIndicator(playerAngle - AREA_ANGLE);
		CreateIndicator(playerAngle + AREA_ANGLE);

		// spawn opposite side
		yield return new WaitForSeconds(1);
		playerAngle = (playerAngle + 180) % 360;
		CreateIndicator(playerAngle);
		CreateIndicator(playerAngle - AREA_ANGLE);
		CreateIndicator(playerAngle + AREA_ANGLE);
	}

	private IEnumerator ClockWise()
	{
		bool clockwise = Random.value <= 0.5f;
		float playerAngle = GetPlayerAngle();
		for (int i = (clockwise ? 0 : 6); i != (clockwise ? 6 : 0); i += (clockwise ? 1 : -1))
		{
			CreateIndicator((i * AREA_ANGLE + playerAngle) % 360);
			yield return new WaitForSeconds(clockwiseDelay);
		}
	}

	private IEnumerator Checkers()
	{
		float playerAngle = GetPlayerAngle();
		bool playerFirst = Random.value <= 0.5f;
		if(playerFirst)
		{
			Checkers_PlayerSide(playerAngle);
			yield return new WaitForSeconds(checkersDelay);
			Checkers_PlayerInverted(playerAngle);
		}
		else
		{
			Checkers_PlayerInverted(playerAngle);
			yield return new WaitForSeconds(checkersDelay);
			Checkers_PlayerSide(playerAngle);
		}
	}

	private void Checkers_PlayerSide(float playerAngle)
	{
		CreateIndicator(playerAngle);
		CreateIndicator(playerAngle - AREA_ANGLE * 2);
		CreateIndicator(playerAngle + AREA_ANGLE * 2);
	}

	private void Checkers_PlayerInverted(float playerAngle)
	{
		playerAngle = (playerAngle + 180) % 360;
		CreateIndicator(playerAngle);
		CreateIndicator(playerAngle - AREA_ANGLE * 2);
		CreateIndicator(playerAngle + AREA_ANGLE * 2);
	}

	private IEnumerator TargetPlayer()
	{
		int count = Mathf.FloorToInt(Random.value * 3) + 3;
		for(int i = 0; i != count; i++)
		{
			float playerAngle = GetPlayerAngle();
			CreateIndicator(playerAngle);
			yield return new WaitForSeconds(targetPlayerDelay);
		}
	}

	private float GetPlayerAngle()
	{
		return (Mathf.Atan2(boss.target.position.y, boss.target.position.x) * Mathf.Rad2Deg + 360) % 360;
	}


	public override int WeightAttack()
	{
		int value = 50;

		return value;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 250, 75, 25), "Triangle"))
			StartAttack();
		*/
	}
}

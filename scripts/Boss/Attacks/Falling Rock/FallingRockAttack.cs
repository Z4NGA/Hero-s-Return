using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockAttack : Attack
{
	public int numberOfRocks = 3;
	public float rockDelay = 1;

	public GameObject fallingRock;

	private Boss boss;

    void Start()
    {
		boss = GetComponent<Boss>();
	}

	public override void StartAttack()
	{
		attackStage = AttackStage.Attack;
		StartCoroutine("StartFallingRocks");
	}

    public IEnumerator StartFallingRocks()
	{
		int rocksThrown = 0;
		while(rocksThrown < numberOfRocks)
		{
			GameObject g = Instantiate(fallingRock, boss.target.position, Quaternion.identity);
			FallingRock rock = g.GetComponent<FallingRock>();
			rock.boss = boss;
			rock.StartAttack();
			rocksThrown++;
			yield return new WaitForSeconds(rockDelay);
		}
		yield return new WaitForSeconds(1);
		attackStage = AttackStage.Finished;
	}

	public override int WeightAttack()
	{
		int value = 50;

		return value;
	}

	private void OnGUI()
	{
		/*if(GUI.Button(new Rect(10, 220, 75, 25), "Rocks!"))
		{
			StartAttack();
		}*/
	}
}

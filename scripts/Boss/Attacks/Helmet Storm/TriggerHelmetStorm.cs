using System.Collections;
using UnityEngine;

public class TriggerHelmetStorm : Attack
{
	public GameObject helmetStorm;
	public float windDownTime = 2.0f;

	Animator anim;
	Boss boss;

    void Start()
    {
		anim = GetComponent<Animator>();
		boss = GetComponent<Boss>();
    }

	public override void StartAttack()
	{
		attackStage = AttackStage.Attack;
		StartCoroutine("StartHelmetStorm");
	}

	public IEnumerator StartHelmetStorm()
	{
		anim.SetTrigger("vanish");
		yield return new WaitForSeconds(0.25f);
		soundEngine.play_sound(soundEngine.sound_type.boss_vanish);
		yield return new WaitForSeconds(0.75f);
		transform.position = new Vector3(100, 100);
		GameObject g = Instantiate(helmetStorm, Vector3.zero, Quaternion.identity);
		HelmetStorm storm = g.GetComponent<HelmetStorm>();
		storm.boss = boss;
		storm.StartAttack();
		float waitTime = g.GetComponent<HelmetStorm>().spawnDuration + windDownTime;
		yield return new WaitForSeconds(waitTime);
		transform.position = Vector3.zero;
		yield return StartCoroutine(boss.TriggerStareC(1f));

		attackStage = AttackStage.Downtime;
		yield return new WaitForSeconds(1.5f);

		attackStage = AttackStage.Finished;
	}

	public override int WeightAttack()
	{
		int value = 50;

		return value;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 170, 75, 25), "Helmet Storm"))
		{
			StartAttack();
		}*/
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInHelmet : Attack
{

	Animator anim;
	Boss boss;

	private void Start()
	{
		anim = GetComponent<Animator>();
		boss = GetComponent<Boss>();
	}

	public override void StartAttack()
	{
		StartCoroutine("ShowCat");
	}

	IEnumerator ShowCat()
	{
		boss.canBeDamaged = false;
		anim.SetTrigger("show_cat");
		yield return new WaitForSeconds(0.3f);
		soundEngine.play_sound(soundEngine.sound_type.meow_question);
		yield return new WaitForSeconds(2.5f);
		soundEngine.play_sound(soundEngine.sound_type.meow_angry);
		yield return new WaitForSeconds(0.5f);
		boss.canBeDamaged = true;
		yield return new WaitForSeconds(1);
		attackStage = AttackStage.Finished;
	}

	public override int WeightAttack()
	{
		return -1;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 360, 75, 25), "Show Cat"))
			StartAttack();
		*/
	}
}

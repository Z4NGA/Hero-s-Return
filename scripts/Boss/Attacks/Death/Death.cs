using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Death : Attack
{
	public LevelController levelController;
	public GameObject explosionParticles;
	public GameObject lightningStrike;

	public GameObject rewardCat;
	public GameObject rewardParticles;
	public TextMeshProUGUI rewardTitle;
	public TextMeshProUGUI rewardSubtext;
	public GameObject healthCanvas;
	public float dramaticShaking;
	public float shakingStrength;

	public GameObject winCanvas;
	public GameObject[] additionalGUI;


	public CameraShake shaker;

	Health health;
	Boss boss;

	public Vector3 deathPosition;

	private void Start()
	{
		health = GetComponent<Health>();
		boss = GetComponent<Boss>();

		rewardTitle.color = new Color(rewardTitle.color.r, rewardTitle.color.g, rewardTitle.color.b, 0);
		rewardSubtext.color = new Color(rewardTitle.color.r, rewardTitle.color.g, rewardTitle.color.b, 0);
	}

	public override void StartAttack()
	{
		StartCoroutine("Attack");
	}

	IEnumerator Attack()
	{
		winCanvas.SetActive(true);
		healthCanvas.SetActive(false);
		boss.locked = true; // das geht, weil death niemals attackStage auf Finished setzt -> Kampf ist vorbei
		StartCoroutine(LightningStrikes());
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_death_scream, 10);
		shaker.Shake(dramaticShaking, shakingStrength);
		yield return new WaitForSeconds(dramaticShaking);

		transform.position = new Vector3(100, 100);
		levelController.GetComponent<MakeLavaCold>().Activate();
		GameObject g = Instantiate(explosionParticles, deathPosition + new Vector3(0, 2), Quaternion.identity);
		g.GetComponent<ParticleSystem>().Play();
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_death, 10);
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_death_armor, 10);
		yield return new WaitForSeconds(1);
		Destroy(g);
		soundEngine.play_sound(soundEngine.sound_type.boss_vanish);
		yield return new WaitForSeconds(0.25f);

		Instantiate(rewardCat, Vector3.zero, Quaternion.identity);
		StartCoroutine(FadeRewardText(rewardTitle, 0, 0.3f));
		StartCoroutine(FadeRewardText(rewardSubtext, 3, 0.3f));
		GameObject confetti = Instantiate(rewardParticles, Vector3.zero, Quaternion.identity);
		yield return new WaitForSeconds(5);
		foreach (GameObject ui in additionalGUI)
			ui.SetActive(true);
	}

	private IEnumerator LightningStrikes()
	{
		float waitTime = 1.2f;
		for(int i = 0; i != 7; i++)
		{
			soundEngine.play_sound(soundEngine.sound_type.boss_orb_lightning);
			GameObject lightning = Instantiate(lightningStrike, deathPosition, Quaternion.AngleAxis(Random.value * 360, Vector3.forward));
			float scale = Random.value * 3 + 6;
			lightning.transform.localScale = new Vector3(scale, scale, 1);
			Destroy(lightning, 0.1f);
			yield return new WaitForSeconds(waitTime);
			waitTime -= 1 / Mathf.Exp(waitTime);
		}
	}

	private IEnumerator FadeRewardText(TextMeshProUGUI tmp, float waitTime, float speed)
	{
		yield return new WaitForSeconds(waitTime);
		float time = 0;
		float fadeTime = speed;
		while (time <= 1)
		{
			tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, time);
			time += Time.deltaTime * fadeTime;
			yield return null;
		}
	}

	public override int WeightAttack()
	{
		int value = 0;
		if (health.CurrentHealth <= 0)
			value += 102;
		return value;
	}
}

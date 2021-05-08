using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DarkOrbAttack : Attack
{
	public GameObject lightning;
	public GameObject orb;
	private Transform orbTransform;
	public GameObject lightningHitIndicator;

	private float damageRadius;
	[Header("Orb")]
	public float orbFlashIntensity;
	public Vector2 orbPosition;
	[Header("Lightning")]
	public Vector2 attacks = new Vector2(3, 5);
	public float lightningDelay;
	public float lightningHitDelay = 0.25f;
	public float lightningLifetime = 0.1f;

	Boss boss;
	Animator anim;

    void Start()
    {
		boss = GetComponent<Boss>();
		anim = GetComponent<Animator>();
		damageRadius = lightningHitIndicator.GetComponent<Light2D>().pointLightOuterRadius;
    }

	public override void StartAttack()
	{
		attackStage = AttackStage.Attack;
		StartCoroutine(Attack());
	}
	
	private IEnumerator Attack()
	{
		int count = Mathf.FloorToInt(Random.value * (attacks.y - attacks.x) + 3);
		anim.SetBool("raise_helmet", true);
		orbTransform = Instantiate(orb, transform.position + new Vector3(orbPosition.x, orbPosition.y), Quaternion.identity).transform;
		for (int i = 0; i != count; i++)
		{
			// spawn indicator, follow player
			GameObject indicator = Instantiate(lightningHitIndicator, boss.target.position, Quaternion.identity);
			indicator.GetComponent<LightningHitIndicator>().Follow(boss.target, lightningDelay);
			yield return new WaitForSeconds(lightningDelay);

			// orb flash indicates cast point, indicator stands still
			float playerAngle = (GetPlayerAngleFromOrb(orbTransform.position) - 180) % 360;
			Vector2 onCastPlayerPos = boss.target.position;
			indicator.GetComponent<LightningHitIndicator>().StopFollowing();
			OrbFlash flash = orbTransform.GetComponent<OrbFlash>();
			StartCoroutine(flash.Flash(orbFlashIntensity));
			yield return new WaitForSeconds(lightningHitDelay);

			// lightning hit
			GameObject lightningInstance = Instantiate(lightning, orbTransform.position, Quaternion.AngleAxis(playerAngle, Vector3.forward));
			ScaleLightning(orbTransform.position, lightningInstance.transform, onCastPlayerPos);
			soundEngine.play_sound(soundEngine.sound_type.boss_orb_lightning);
			DamagePlayer(onCastPlayerPos);
			Destroy(lightningInstance, lightningLifetime);
			Destroy(indicator, lightningLifetime);
		}
		yield return new WaitForSeconds(1.5f);
		attackStage = AttackStage.Finished;
		anim.SetBool("raise_helmet", false);
		Destroy(orbTransform.gameObject);
	}

	private float GetPlayerAngleFromOrb(Vector3 orbPosition)
	{
		Vector3 shifted = boss.target.position - orbPosition;
		return Mathf.Atan2(shifted.y, shifted.x) * Mathf.Rad2Deg;
	}

	private void ScaleLightning(Vector3 orbPosition, Transform lightning, Vector2 target)
	{
		float distance = Vector2.Distance(target, orbPosition);
		float yScaleFactor = (8 / Mathf.Exp(0.3f * distance)) + 1;

		lightning.localScale = new Vector3(distance, distance * yScaleFactor, 1);
	}

	private void DamagePlayer(Vector2 hitPosition)
	{
		float distance = Vector2.Distance(boss.target.position, hitPosition);
		if(distance <= damageRadius)
		{
			popup_damage.popup(boss.damage_LightningStrike, boss.target.position, true);
			boss.target.GetComponent<in_targetable>().take_damage(boss.damage_LightningStrike);
		}
	}

	public override int WeightAttack()
	{
		int value = 50;

		return value;
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 400, 75, 25), "Dark Orb"))
			StartAttack();
		*/
	}
}

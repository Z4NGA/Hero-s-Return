using UnityEngine;
using System.Collections;

public class ChargeLightningOrb : MonoBehaviour
{
	public Boss boss;

	[Header("Eletrified")]
	public GameObject lightningDebuff;
	public float debuffDuration;
	public float debuffIntervall = 3;
	private Coroutine debuff = null;

	[Header("Flash")]
	public float flashIntensity = 1.2f;
	public float movementDelay = 0.25f;

	private Rigidbody2D rb;
	private OrbFlash flash;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		flash = GetComponent<OrbFlash>();
	}

	public IEnumerator Move(Vector2 start, Vector2 end, float speed)
	{
		soundEngine.play_sound(soundEngine.sound_type.boss_stare);
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_charge_electric_charge, 5);
		StartCoroutine(flash.Flash(flashIntensity));
		//soundEngine.play_sound(soundEngine.sound_type.boss_charge_crackling);
		yield return new WaitForSeconds(movementDelay);
		float i = 0;
		while (i <= 1.05)
		{
			rb.MovePosition(Vector3.Lerp(start, end, i));
			i += Time.deltaTime * speed;
			yield return null;
		}
	}

	public IEnumerator MoveWithoutSound(Vector2 start, Vector2 end, float speed)
	{
		StartCoroutine(flash.Flash(flashIntensity));
		yield return new WaitForSeconds(movementDelay);
		float i = 0;
		while (i < 1)
		{
			rb.MovePosition(Vector3.Lerp(start, end, i));
			i += Time.deltaTime * speed;
			yield return null;
		}
	}

	public void TakeDamage(float amount)
	{
		boss.TakeDamage(amount);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("Player"))
		{
			soundEngine.play_sound(soundEngine.sound_type.boss_charge_ball_hit);
			popup_damage.popup(boss.damage_LightningCharge, boss.target.position, true);
			boss.target.GetComponent<in_targetable>().take_damage(boss.damage_LightningCharge);
			if(debuff == null)
				debuff = StartCoroutine(StunPlayer());
		}
	}

	private IEnumerator StunPlayer()
	{
		for(int i = 0; i != 3; i++)
		{
			boss.target.GetComponent<movement>().knockback(debuffDuration, 0, transform);
			soundEngine.play_sound(soundEngine.sound_type.boss_charge_ball_hit);
			GameObject g = Instantiate(lightningDebuff, boss.target.position, Quaternion.AngleAxis(90, Vector3.forward));
			g.transform.localScale = new Vector3(2, 2, 1);
			g.transform.position += new Vector3(0, 1);
			Destroy(g, 0.1f);
			yield return new WaitForSeconds(debuffIntervall);
		}
		debuff = null;
	}
}

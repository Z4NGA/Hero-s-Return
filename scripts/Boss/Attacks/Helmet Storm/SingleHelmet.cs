using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHelmet : MonoBehaviour
{
	public float xSpeed;
	public float ySpeed;
	public float jumpFrequency = 1;
	public float jumpHeight = 1;

	private Rigidbody2D rb;
	private float yPos;
	private float xPos;
	private float yOffset;

	public float knockbackForce;
	public float stunTime;

	public float damageFactor = 0.1f;

	private bool soundPlaying = false;

	public Boss boss;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		yPos = rb.position.y;
		xPos = rb.position.x;
		yOffset = Random.value * 10;
	}

	private void FixedUpdate()
	{
		rb.MovePosition(new Vector2(transform.localPosition.x + xSpeed * Time.fixedDeltaTime,
			Mathf.Abs(Mathf.Sin((Time.time + yOffset) * jumpFrequency) * jumpHeight) * (ySpeed * Time.fixedDeltaTime) + yPos));

		float yDistance = Mathf.Abs(Mathf.Abs(rb.position.y) - Mathf.Abs(yPos));
		if (!soundPlaying && yDistance < 0.05f)
		{
			float t = Mathf.InverseLerp(xPos, -xPos, rb.position.x);
			soundEngine.play_from_side(soundEngine.sound_type.helmet_clank, t);
			soundPlaying = true;
		}

		if (yDistance > 1)
			soundPlaying = false;


		if (Mathf.Abs(rb.position.x) > 10)
			Destroy(gameObject);
	}

	public void TakeDamage(float amount)
	{
		boss.TakeDamage(amount * damageFactor);
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			popup_damage.popup(boss.damage_SingleHelmet, collision.transform.position, true);
			collision.transform.GetComponent<in_targetable>().take_damage(boss.damage_SingleHelmet);
			collision.transform.GetComponent<movement>().knockback(stunTime, knockbackForce, transform);
		}
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss : MonoBehaviour
{
	public Transform target;
	public Material bossMaterial;
	public Animator animator;

	public Transform starePosition;
	public GameObject stareEffect;

	public Attack[] attacks;
	private Attack catAttack;
	private int usedAttack;

	[Header("Testing")]
	public bool debugControl = false;
	public bool locked = false;
	public bool useCat = true;
	public float catUseChance = 0.3f;

	[Header("Player Damage")]
	public bool canBeDamaged = true;
	public float meleeDamageFactor = 0.5f;
	public float bulletDamageFactor = 0.1f;

	new AudioSource audio;
	Health health;
	Shield shield;

	[Header("Attack damage values")]
	public int damage_SingleHelmet = 50;
	public int damage_FallingRock = 100;
	public int damage_RockLavaHole = 20;
	public int damage_BossTouch = 20;
	public int damage_Triangle = 50;
	public int damage_LightningStrike = 50;
	public int damage_LightningCharge = 50;
	public float damage_LightningChargeFactor = 1.2f;

	[Header("Touch")]
	public float stunDuration = 0.5f;
	public float knockbackForce = 100;

	private void Start()
	{
		animator = GetComponent<Animator>();
		attacks = GetComponents<Attack>();
		audio = GetComponent<AudioSource>();
		catAttack = attacks[attacks.Length - 1];
		locked = true;
		transform.position = new Vector3(100, 100, 0);
		health = GetComponent<Health>();
		shield = GetComponentInChildren<Shield>();
	}

	void Update()
    {
		if (debugControl)
			return;

		if(!locked)
		{
			SetLookLeftOrRight();
			SetLookUpOrDown();
		}


		Debug.DrawLine(transform.position, transform.position + transform.right * 2, Color.blue);

		CheckAttaks();
	}

	// check to mirror the boss sprite if the player moves to the other side
	private bool lookingRight = true;
	private void SetLookLeftOrRight()
	{
		bool tmpLook = lookingRight;
		if (target.position.x < transform.position.x)
			lookingRight = true;
		else if (target.position.x > transform.position.x)
			lookingRight = false;
		
		if(tmpLook != lookingRight)
		{
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	//check for player above or below the boss
	private bool lookingDown = true;
	private void SetLookUpOrDown()
	{
		bool tmpLook = lookingDown;
		if (target.position.y < transform.position.y)
			lookingDown = true;
		else if (target.position.y > transform.position.y)
			lookingDown = false;

		if (tmpLook != lookingDown)
			animator.SetBool("look_up", !animator.GetBool("look_up"));
	}

	private void CheckAttaks()
	{
		if(attacks[usedAttack].attackStage == AttackStage.Finished)
		{
			locked = false;
			attacks[usedAttack].ResetAttack();
		}

		if (!locked /*&& attacks[usedAttack].attackStage == AttackStage.Ready*/)
		{
			if (!useCat)
			{
				// Gewichte Attacken
				float[] attackWeights = new float[attacks.Length];
				float maxWeight = 0;
				for (int i = 0; i < attacks.Length; i++)
				{
					float weight = attacks[i].WeightAttack();
					attackWeights[i] = weight;
					if (weight > maxWeight)
						maxWeight = weight;
				}

				string weightString = "Weights are:\n";
				for (int i = 0; i < attacks.Length; i++)
				{
					weightString += attacks[i].GetType().Name + ": " + attackWeights[i] + "\n";
				}
				Debug.Log(weightString);

				// Wähle Attacke mit größtem Gewicht aus
				List<int> possibleAttacks = new List<int>();
				for (int i = 0; i < attackWeights.Length; i++)
				{
					if (attackWeights[i] == maxWeight)
						possibleAttacks.Add(i);
				}

				string pa = "Possible attacks:\n";
				for (int i = 0; i < possibleAttacks.Count; i++)
				{
					pa += attacks[possibleAttacks[i]].GetType().Name + "\n";
				}
				Debug.Log(pa);

				int possibleAttackToBeUsed = 0;
				// Wenn mehr wie eine möglich sind, wähle zufällig
				if (possibleAttacks.Count > 1)
					possibleAttackToBeUsed = (int)Mathf.Floor(Random.value * possibleAttacks.Count);

				Debug.Log("Chosen attack: " + attacks[possibleAttacks[possibleAttackToBeUsed]].GetType().Name);

				// Starte Attacke
				attacks[possibleAttacks[possibleAttackToBeUsed]].StartAttack();
				locked = true;
				usedAttack = possibleAttacks[possibleAttackToBeUsed];
				useCat = Random.value < catUseChance;
			}
			else
			{
				attacks[attacks.Length - 1].StartAttack();
				locked = true;
				usedAttack = attacks.Length - 1;
				useCat = false;
			}
		}
	}

	public void TakeDamage(float amount, bool bullet = false)
	{
		if (canBeDamaged)
		{
			if (bullet)
				health.modifyHealth(-amount * bulletDamageFactor);
			else
				health.modifyHealth(-amount * meleeDamageFactor);
			// play random hurt sound
			int audio = Mathf.FloorToInt(Random.value * 3);
			switch (audio)
			{
				case 0:
					soundEngine.play_sound(soundEngine.sound_type.meow5);
					break;
				case 1:
					soundEngine.play_sound(soundEngine.sound_type.meow6);
					break;
				case 2:
					soundEngine.play_sound(soundEngine.sound_type.meow7);
					break;
			}
		}
		else
			StartCoroutine(shield.Flash());

	}

	public void TriggerStare(float time = 0.3f)
	{
		audio.Play();
		animator.SetTrigger("use_stare");
		GameObject g = Instantiate(stareEffect, starePosition.position, Quaternion.identity);
		ParticleSystem p = g.GetComponent<ParticleSystem>();
		p.Play();
		Destroy(g, time);
	}

	public IEnumerator TriggerStareC(float time = 0.3f)
	{
		audio.Play();
		animator.SetTrigger("use_stare");
		GameObject g = Instantiate(stareEffect, starePosition.position, Quaternion.identity);
		g.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(time);
		Destroy(g);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			popup_damage.popup(damage_BossTouch, collision.transform.position, true);
			collision.transform.GetComponent<in_targetable>().take_damage(damage_BossTouch);
			collision.transform.GetComponent<movement>().knockback(stunDuration, knockbackForce, transform);
			StartCoroutine(shield.Flash());
		}
	}

	private void OnGUI()
	{
		/*
		if(GUI.Button(new Rect(100, 10, 75, 35), "Stare"))
		{
			StartCoroutine("TriggerStareC", 0.3f);
		}
		*/
	}
}

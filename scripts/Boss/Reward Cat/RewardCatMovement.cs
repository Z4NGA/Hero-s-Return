using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCatMovement : MonoBehaviour
{
	public float speed;
	public float yStart = 2;

	public AudioClip[] audioClips;

	float distance;
	float angle;
	float angleCounter;

	Animator anim;
	new AudioSource audio;

	private void Start()
	{
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		audio.volume = 0.5f;

		transform.position = new Vector2(0, yStart);
		distance = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2));
		angle = Mathf.Atan2(transform.position.y, transform.position.x);
		angleCounter = Mathf.PI / 8;
	}

	private void Update()
	{
		float angleDiff = speed * Time.deltaTime;
		angle += angleDiff;
		float x = distance * Mathf.Cos(angle);
		float y = distance * Mathf.Sin(angle);

		anim.SetFloat("moveX", x - transform.position.x);
		anim.SetFloat("moveY", y - transform.position.y);

		angleCounter += angleDiff;
		if (angleCounter >= Mathf.PI / 4)
		{
			angleCounter = 0;
			Meow();
		}

		transform.position = new Vector2(x, y);
	}

	private void Meow()
	{
		if(Random.value < 0.1f)
		{
			int roll = Mathf.RoundToInt(Random.value * (audioClips.Length - 1));
			audio.clip = audioClips[roll];
			audio.Play();
		}
	}
}

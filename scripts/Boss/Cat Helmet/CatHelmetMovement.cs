using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHelmetMovement : MonoBehaviour
{
	public float rattleInterval = 1f;
	public float rattleWaitTime = 0.05f;
	public float rattleAngle = 30;
	public int rattles = 4;

	new AudioSource audio;
	public AudioClip[] randomMeows;

	private float passedTime = 0;

	private void Start()
	{
		passedTime = rattleInterval / 2;
		audio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (passedTime >= rattleInterval)
		{
			StartCoroutine("Rattle");
			audio.clip = randomMeows[Mathf.RoundToInt(Random.value * (randomMeows.Length - 1))];
			audio.Play();
			passedTime = 0;
		}
		passedTime += Time.deltaTime;
	}

	IEnumerator Rattle()
	{
		int i = 0;
		while(i < rattles)
		{
			if (i == rattles - 1)
				transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
			else if (i % 2 == 0)
				transform.rotation = Quaternion.AngleAxis(rattleAngle, Vector3.forward);
			else
				transform.rotation = Quaternion.AngleAxis(-rattleAngle, Vector3.forward);
			i++;

			yield return new WaitForSeconds(rattleWaitTime);
		}
	}
}

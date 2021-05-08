using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSpawner : MonoBehaviour
{
	public GameObject confettiSpray;
	public float delay;
	public float radius;
	public int amount;

	private void Start()
	{
		StartCoroutine(SpawnConfetti());
	}

	public IEnumerator SpawnConfetti()
	{
		for(int i = 0; i != amount; i++)
		{
			GameObject g = Instantiate(confettiSpray, Random.insideUnitCircle * radius, Quaternion.Euler(-135, 0, 0), transform);
			yield return new WaitForSeconds(Random.value * delay);
		}
	}
}

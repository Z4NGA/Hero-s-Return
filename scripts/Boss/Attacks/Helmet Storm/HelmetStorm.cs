using System.Collections;
using UnityEngine;

public class HelmetStorm : MonoBehaviour
{
	public bool attackFromLeft;
	public float spawnDuration = 6;
	public float spawnDelay = 0.2f;

	public GameObject helmet;

	public Transform upperSpawnPoint;
	public Transform lowerSpawnPoint;

	public Boss boss;


	public void StartAttack()
	{
		attackFromLeft = Random.value > 0.5 ? true : false;
		StartCoroutine("StartHelmetStorm");
	}

	public IEnumerator StartHelmetStorm()
	{
		float i = 0;
		while(i < spawnDuration)
		{
			SpawnHelmet();
			i += Time.deltaTime + spawnDelay;
			yield return new WaitForSeconds(spawnDelay);
		}
		Destroy(gameObject, 10);
	}

	void SpawnHelmet()
	{
		if(attackFromLeft)
		{
			GameObject g = Instantiate(helmet, GetRandomPointOnEdge(), Quaternion.identity);
			g.GetComponent<SpriteRenderer>().flipX = true;
			g.GetComponent<SingleHelmet>().boss = boss;
		}
		else
		{
			Vector2 spawnPoint = GetRandomPointOnEdge();
			spawnPoint = new Vector2(-spawnPoint.x, spawnPoint.y);
			GameObject g = Instantiate(helmet, spawnPoint, Quaternion.identity);
			g.GetComponent<SingleHelmet>().xSpeed = -g.GetComponent<SingleHelmet>().xSpeed;
			g.GetComponent<SingleHelmet>().boss = boss;
		}
	}

	Vector2 GetRandomPointOnEdge()
	{
		float random = Random.value;
		Vector2 maxDistance = upperSpawnPoint.position - lowerSpawnPoint.position;
		return new Vector2(lowerSpawnPoint.position.x, lowerSpawnPoint.position.y) + maxDistance * random;
	}
}

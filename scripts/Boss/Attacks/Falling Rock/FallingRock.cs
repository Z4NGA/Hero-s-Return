using System.Collections;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
	public GameObject hole;

	public LayerMask playerLayer;

	public float speed;
	public float hitRadius;

	public Transform rock;
	public Transform shadow;

	public GameObject explosionParticles;
	public float particlePlayTime = 1.0f;

	public Boss boss;
	CameraShake shaker;
	public float shakeDuration;
	public float shakeStrenght;

	public float knockbackDuration;
	public float knockbackForce;

	public void StartAttack()
	{
		shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
		rock.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
		StartCoroutine("MakeRockFall");
	}

	IEnumerator MakeRockFall()
	{
		while(rock.position.y > shadow.position.y)
		{
			rock.position -= new Vector3(0, speed * Time.deltaTime, 0);
			yield return null;
		}
		Destroy(rock.gameObject);
		GameObject g = Instantiate(explosionParticles, shadow.position, Quaternion.identity);
		g.GetComponent<ParticleSystem>().Play();
		Destroy(shadow.gameObject);
		soundEngine.play_sound(soundEngine.sound_type.rock_break);
		shaker.Shake(shakeDuration, shakeStrenght);

		GameObject holeInstance = Instantiate(hole, shadow.position, Quaternion.identity);
		holeInstance.GetComponent<Hole>().boss = boss;

		// see if the rock hit
		Collider2D playerHit = Physics2D.OverlapCircle(shadow.position, hitRadius, playerLayer);
		if(playerHit != null)
		{
			popup_damage.popup(boss.damage_FallingRock, playerHit.transform.position, true);
			playerHit.transform.GetComponent<in_targetable>().take_damage(boss.damage_FallingRock);
			playerHit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			playerHit.GetComponent<movement>().knockback(knockbackDuration, knockbackForce, transform);
		}
		yield return new WaitForSeconds(particlePlayTime);
		Destroy(g);
		Destroy(gameObject, 2);
	}
}

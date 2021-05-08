using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Indicator_Triangle : MonoBehaviour
{
	public Transform perimeter;
	public Transform area;

	public GameObject explosionParticles;
	private float duration;
	public float lightFadeSpeed = 3;

	CameraShake shaker;
	public float shakeDuration;
	public float shakeStrenght;

	private float triangleCreationAngle;
	private Boss boss;

	private const float AREA_ANGLE = 60;

	private void Start()
	{
		shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	}

	public void StartIndicator(float duration, Boss boss, float initialPlayerAngle)
	{
		this.duration = duration;
		this.triangleCreationAngle = initialPlayerAngle % 360;
		Debug.Log(triangleCreationAngle);
		this.boss = boss;

		area.localScale = new Vector3(0, 0, 1);
		perimeter.localScale = new Vector3(1, 1, 1);
		StartCoroutine("ScaleArea");
	}

	private IEnumerator ScaleArea()
	{
		GameObject particles = Instantiate(explosionParticles, Vector3.zero - new Vector3(0, 0.1f, 0), transform.rotation * Quaternion.AngleAxis(30, Vector3.forward), transform);
		particles.GetComponent<ParticleSystem>().Play();

		float scalePerSecond = 1 / duration;
		float currentScale = 0;
		while (currentScale < 1)
		{
			currentScale += scalePerSecond * Time.deltaTime;
			area.localScale = new Vector3(currentScale, currentScale, 1);
			yield return null;
		}
		soundEngine.play_sound(soundEngine.sound_type.boss_magic_field);
		DamagePlayer();
		shaker.Shake(shakeDuration, shakeStrenght);
		Destroy(perimeter.gameObject);
		Destroy(area.gameObject);

		Light2D light = particles.GetComponent<Light2D>();
		StartCoroutine(FadeLight(light));

		Destroy(particles, 5);
		Destroy(gameObject, 5);
	}

	private IEnumerator FadeLight(Light2D light)
	{
		float time = 0;
		while(time < 1)
		{
			light.intensity = 1 - time;
			time += Time.deltaTime * lightFadeSpeed;
			yield return null;
		}
	}

	private void DamagePlayer()
	{
		float playerAngle = Mathf.Atan2(boss.target.position.y, boss.target.position.x) * Mathf.Rad2Deg;
		playerAngle = (playerAngle + 360) % 360;

		float angleDiff = (triangleCreationAngle - playerAngle + 180 + 360) % 360 - 180;

		if(angleDiff <= AREA_ANGLE / 2 && angleDiff >= -(AREA_ANGLE / 2))
		{
			popup_damage.popup(boss.damage_Triangle, boss.target.transform.position, true);
			boss.target.GetComponent<in_targetable>().take_damage(boss.damage_Triangle);
		}
	}
}

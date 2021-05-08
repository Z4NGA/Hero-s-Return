using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class LightningHitIndicator : MonoBehaviour
{
	public Transform target;
	private bool follow = true;

	new Light2D light;
	private float maxIntensity;
	private float maxRadius;

	private void Update()
	{
		if (follow)
			transform.position = target.position;
	}

	public void Follow(Transform target, float duration)
	{
		this.target = target;
		light = GetComponent<Light2D>();
		maxIntensity = light.intensity;
		maxRadius = light.pointLightOuterRadius;
		StartCoroutine(ScaleIntensity(duration));
		//StartCoroutine(ScaleRadius(duration));
	}

	public void StopFollowing()
	{
		follow = false;
	}

	private IEnumerator ScaleIntensity(float duration)
	{
		float speed = maxIntensity / duration;
		float time = 0;
		while (time <= maxIntensity)
		{
			light.intensity = time;
			time += Time.deltaTime * speed;
			yield return null;
		}
	}

	private IEnumerator ScaleRadius(float duration)
	{
		float speed = maxRadius / duration;
		float time = 0;
		while (time <= maxRadius)
		{
			light.pointLightOuterRadius = time;
			time += Time.deltaTime * speed;
			yield return null;
		}
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class OrbFlash : MonoBehaviour
{
	public Light2D orbLight;

	public IEnumerator Flash(float intensityFactor)
	{
		float initialIntensity = orbLight.intensity;
		//soundEngine.play_sound(soundEngine.sound_type.boss_shield);
		float time = 0;
		while (time < .5f)
		{
			orbLight.intensity = initialIntensity + (1 - Mathf.Abs(time - 0.25f) * 4) * intensityFactor;
			time += Time.deltaTime;
			yield return null;
		}
		orbLight.intensity = initialIntensity;
	}
}

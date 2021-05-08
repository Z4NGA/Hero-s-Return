using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Shield : MonoBehaviour
{
	Light2D shieldLight;
	SpriteRenderer shieldSprite;
	public float flashIntensity = 3;
	public float duration = 0.5f;

    void Start()
    {
		shieldLight = GetComponent<Light2D>();
		shieldSprite = GetComponent<SpriteRenderer>();
		shieldSprite.enabled = false;
    }

	public IEnumerator Flash()
	{
		soundEngine.play_sound(soundEngine.sound_type.boss_shield);
		soundEngine.play_sound(soundEngine.sound_type.boss_laugh);
		shieldSprite.enabled = true;
		float time = 0;
		while(time < .5f)
		{
			shieldLight.intensity = (1 - Mathf.Abs(time - 0.25f) * 4) * flashIntensity;
			time += Time.deltaTime;
			yield return null;
		}
		shieldLight.intensity = 0;
		shieldSprite.enabled = false;
	}

	private void OnGUI()
	{
		/*if(GUI.Button(new Rect(10, 500, 75, 25), "Light!"))
		{
			StartCoroutine(Flash());
		}*/
	}

}
